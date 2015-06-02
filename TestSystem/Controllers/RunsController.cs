﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TestSystem.Core;
using TestSystem.DataAccess;
using TestSystem.Models.RunsModels;

namespace TestSystem.Controllers
{
    public class RunsController : Controller
    {
        [Authorize]
        public ActionResult StartTestRun()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult StartTestRun(RunDetailsViewModel runDetails)
        {
            var regex = new Regex("^([0-9]|[0-4][0]|[0-3][0-9])$");

            if (!ModelState.IsValid)
            {
                return View(runDetails);
            }
            if (runDetails.RepeatNumber == null ||
                !regex.IsMatch(runDetails.RepeatNumber))
            {
                ViewBag.ReRun = "Re-run number is invalid";
                return View(runDetails);
            }

            using (var context = new Entities())
            {
                if (context.Algorithms.Where
                    (x => x.Name == runDetails.AlgorithmName).Count() == 0)
                {
                    ModelState.AddModelError("AlgorithmName",
                   "Can't find this algorithm.");
                    return View(runDetails);
                }
                var testSet = context.TestSets.Where
                    (x => x.Name == runDetails.TestName);

                if (testSet.Count() == 0)
                {
                    ModelState.AddModelError("TestName",
                       "Can't find this test set.");
                    return View(runDetails);
                }
                else
                {
                    var date = DateTime.Now;
                    var userId = context.Users.FirstOrDefault<Users>
                    (x => x.UserName.ToLower() == User.Identity.Name.ToLower()).Id;
                    var testRun = new TestRuns()
                    {
                        DateOfRun = date,
                        UserId = userId,
                        AlgorithmId = context.Algorithms.FirstOrDefault
                        (x => x.Name.ToLower() == runDetails.AlgorithmName).Id,
                        TestSetId = context.TestSets.FirstOrDefault
                        (x => x.Name.ToLower() == runDetails.TestName).Id,
                        RocCurveCalc = runDetails.RocCurveRequired == "true" ? true : false,
                        Status = "In progress",
                        ReTeachNum = Convert.ToInt32(runDetails.RepeatNumber)
                    };

                    context.TestRuns.Add(testRun);
                    context.SaveChanges();

                    var currentRun = context.TestRuns.Where(x => (x.UserId == userId)).OrderByDescending(x => x.Id).ToArray()[0];
                    if (currentRun != null)
                    {
                        runDetails.RunNumber = currentRun.Id;
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }

            return RedirectToAction("UploadResultsFile", "Runs", new { runId = runDetails.RunNumber });
        }

        [Authorize]
        [HttpGet]
        public ActionResult UploadResultsFile(int runId)
        {
            var model = new UploadResultViewModel();

            using (var context = new Entities())
            {
                var run = context.TestRuns.Find(runId);
                if (run.Status != "In progress")
                {
                    throw new UnauthorizedAccessException();
                }
                model.RunNumber = runId;
                model.TestName = context.TestSets.Find(run.TestSetId).Name;
                model.AlgorithmName = context.Algorithms.Find(run.AlgorithmId).Name;
                model.RepeatNumber = (run.ReTeachNum == null ||
                    run.ReTeachNum == 0) ? "no re-runs"
                    : run.ReTeachNum.ToString();
                model.RocCalc = run.RocCurveCalc == true ? "yes" : "no";
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadResultsFile(UploadResultViewModel model)
        {
            var regex = new Regex("^([0-9]|[0-9][0-9]|[0-9][0-9][0-9]|[0-9][0-9][0-9][0-9]|[0-9][0-9][0-9][0-9][0-9])$");

            if (model.RocCalc == "yes" && !regex.IsMatch(model.RocClassNumber))
            {
                ModelState.AddModelError("CustomErrorRoc", "Incorrect class number.");
                return View(model);
            }

            if (model.file == null)
            {
                ModelState.AddModelError("CustomError", "Please, attach test results file.");
                return View(model);
            }
            else
            {
                using (var context = new Entities())
                {
                    var run = context.TestRuns.Find(model.RunNumber);

                    var results = new TestResults()
                    {
                        AlgorithmId = context.Algorithms
                        .Where(x => x.Name == model.AlgorithmName).ToArray()[0].Id,
                        TestRunId = model.RunNumber
                    };

                    var test = context.TestSets.Where(x => x.Name == model.TestName).ToArray()[0];
                    try
                    {
                        var analyzer = new TestResultsAnalyzer();
                        if (model.RocCalc == "no")
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                model.file.InputStream.CopyTo(ms);
                                ms.Seek(0, SeekOrigin.Begin);

                                var statistics = analyzer.CalcErrorPercentage
                                    (test.ExpectedResults, ms, Convert.ToInt32(model.RepeatNumber) + 1);

                                results.CorrectRate = statistics.ToString("0.000");
                                context.TestResults.Add(results);

                                context.SaveChanges();

                                run.Status = "Results saved";

                                context.SaveChanges();

                                return RedirectToAction("RunResults", new { resultsId = results.Id });
                            }
                        }
                        else if (model.RocCalc == "yes")
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                model.file.InputStream.CopyTo(ms);
                                ms.Seek(0, SeekOrigin.Begin);
                                var rocCreator = new RocCurveCreator();
                                var sbSensivity = new StringBuilder();
                                var sbSpecifity = new StringBuilder();

                                var statistics = rocCreator.GenerateRocCurveCoordinates(test.ExpectedResults, ms, model.RocClassNumber);

                                for (var i = 0; i < statistics.rocCoordinatesSensivity.Count(); i++)
                                {
                                    sbSensivity.Append(statistics.rocCoordinatesSensivity[i].ToString("0.000") + " ");
                                    sbSpecifity.Append(statistics.rocCoordinatesSpecifity[i].ToString("0.000") + " ");
                                }

                                results.RocCoordinatesSensiv = sbSensivity.ToString();
                                results.RocCoordinatesSpecif = sbSpecifity.ToString();
                                results.FN = statistics.FalseNegativeNumber.Average().ToString("0.000");
                                results.FP = statistics.FalsePositiveNumber.Average().ToString("0.000");
                                results.TN = statistics.TrueNegativeNumber.Average().ToString("0.000");
                                results.TP = statistics.TruePositiveNumber.Average().ToString("0.000");
                                results.CorrectRate = statistics.ErrorRate.ToString("0.000");

                                context.TestResults.Add(results);

                                context.SaveChanges();

                                run.Status = "Results saved";

                                context.SaveChanges();

                                return RedirectToAction("RunResultsRoc", new { resultsId = results.Id });
                            }

                            run.Status = "Results saved";
                            run.RocClassNumber = model.RocClassNumber;
                            context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("FileFormatException", "Error occured in results processing. Please check file format.");
                        return View(model);
                    }
                }
            }

            ModelState.AddModelError("FileFormatException", "Server error.");
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult DownloadTestFile(string testName)
        {
            byte[] testFile = null;
            using (var context = new Entities())
            {
                var testSet = context.TestSets.Where
                    (x => x.Name == testName);

                if (testSet.Count() == 0)
                {
                    return View();
                }
                else
                {
                    testFile = testSet.ToArray()[0].Data;
                }
            }

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "TestSet-" + testName + ".txt",
                Inline = false
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(testFile, "text/plain");
        }

        [Authorize]
        public ActionResult RunResultsRoc(int resultsId)
        {
            var model = new RunResultsRocViewModel();

            using (var context = new Entities())
            {
                var results = context.TestResults.Find(resultsId);
                var run = context.TestRuns.Find(results.TestRunId);

                if (!run.RocCurveCalc)
                {
                    return RedirectToAction("RunResults", new { resultsId = resultsId });
                }

                model.CorrectRate = (Convert.ToSingle(results.CorrectRate) * 100).ToString() + "%";
                model.RunNumber = results.TestRunId.ToString();
                var testID = run.TestSetId;
                model.TestName = context.TestSets.Find(testID).Name;
                model.AlgorithmName = context.Algorithms.Find(results.AlgorithmId).Name;
                model.TrueNegativesNumber = Convert.ToSingle(results.TN) * 100 + "%";
                model.TruePositivesNumber = Convert.ToSingle(results.TP) * 100 + "%";
                model.FalseNegativesNumber = Convert.ToSingle(results.FN) * 100 + "%";
                model.FalsePositivesNumber = Convert.ToSingle(results.FP) * 100 + "%";
            }

            return View(model);
        }

        [Authorize]
        public ActionResult RunResults(int resultsID)
        {
            var model = new RunResultsViewModel();

            using (var context = new Entities())
            {
                var results = context.TestResults.Find(resultsID);
                var run = context.TestRuns.Find(results.TestRunId);

                if (run.RocCurveCalc)
                {
                    return RedirectToAction("RunResultsRoc", new { resultsId = resultsID });
                }

                model.CorrectRate = (Convert.ToSingle(results.CorrectRate) * 100).ToString() + "%";
                model.RunNumber = results.TestRunId.ToString();
                var testID = run.TestSetId;
                model.TestName = context.TestSets.Find(testID).Name;
                model.AlgorithmName = context.Algorithms.Find(results.AlgorithmId).Name;
                model.NumberOfRuns = (run.ReTeachNum + 1).ToString();
            }

            return View(model);
        }

        [HttpGet]
        public JsonResult GetCurveCoordinates(int runId)
        {
            var jsonResult = new List<float[]>();
            var jsonResult2 = new List<List<float[]>>();

            using (var context = new Entities())
            {
                var run = context.TestRuns.Find(runId);
                if (!run.RocCurveCalc)
                {
                    return null;
                }

                var results = context.TestResults.Where(x => x.TestRunId == runId).ToArray()[0];

                var rocCoordsSensiv = results.RocCoordinatesSensiv.TrimEnd().Split(' ');
                var rocCoordsSpecif = results.RocCoordinatesSpecif.TrimEnd().Split(' ');

                for (var i = 0; i < rocCoordsSensiv.Count(); i++)
                {
                    jsonResult.Add(new float[] { Convert.ToSingle(rocCoordsSpecif[i]),
                        Convert.ToSingle(rocCoordsSensiv[i]) });
                }
            }

            jsonResult2.Add(jsonResult);
            var defaultCurve = new List<float[]>();
            defaultCurve.Add(new float[] { 0, 0 });
            defaultCurve.Add(new float[] { 1, 1 });

            jsonResult2.Add(defaultCurve);

            return Json(jsonResult2, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AllRuns()
        {
            List<AllRunsViewModel> runsList = new List<AllRunsViewModel>();

            using (var context = new Entities())
            {
                var list = context.TestRuns.Take(40);

                foreach (var run in list)
                {
                    var testRun = new AllRunsViewModel();

                    testRun.AlgorithmName = context.Algorithms.Find(run.AlgorithmId).Name;
                    testRun.DateStart = run.DateOfRun.ToString();
                    testRun.RocCalc = run.RocCurveCalc.ToString();
                    testRun.RocClass = run.RocClassNumber;
                    testRun.RunNumber = run.Id.ToString();
                    testRun.RunsNumber = run.ReTeachNum.ToString();
                    testRun.Status = run.Status;
                    testRun.TestName = context.TestSets.Find(run.TestSetId).Name;
                    testRun.UserName = context.Users.Find(run.UserId).UserName;

                    runsList.Add(testRun);
                }
            }

            return View(runsList);
        }
    }
}