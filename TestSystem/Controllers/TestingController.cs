﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestSystem.Core;
using TestSystem.DataAccess;
using TestSystem.Models;

namespace TestSystem.Controllers
{
    public class TestingController : Controller
    {

        [HttpGet]
        public ActionResult Algorithms()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateAlgorithm()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateAlgorithm(AlgorithmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var context = new Entities())
            {
                if (context.Algorithms.FirstOrDefault<Algorithms>(x => x.Name.ToLower() == model.AlgorithmName) != null)
                {
                    ModelState.AddModelError("AlgorithmName", "This algorithm name is already taken.");
                    return View(model);
                }

                context.Algorithms.Add(new Algorithms
                {
                    Name = model.AlgorithmName,
                    Description = model.AlgorithmDescription,
                    CreatorId = context.Users.FirstOrDefault<Users>
                    (x => x.UserName.ToLower() == User.Identity.Name.ToLower()).Id,
                    DateOfCreation = DateTime.Now
                });

                context.SaveChanges();
            }


            return RedirectToAction("Algorithms", "Testing");
        }

        public ActionResult TestSets()
        {
            return View();
        }      

        [Authorize]
        [HttpGet]
        public ActionResult UploadTest()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadTest(UploadTestViewModel model)
        { 
            if (!ModelState.IsValid)
            {
                return View(model);
            }           

            using (var context = new Entities())
            {
                if (context.TestSets.FirstOrDefault(x => x.Name.ToLower()
                    == model.TestName) != null)
                {
                    ModelState.AddModelError("TestName",
                        "This test set name is already taken.");
                    return View(model);
                }

                byte[] array;
                var contentType = model.file.ContentType;
                var expectedResults = new StringBuilder();

                using (MemoryStream ms = new MemoryStream())
                { 
                    model.file.InputStream.CopyTo(ms);
                    array = ms.GetBuffer();
                    ms.Seek(0, SeekOrigin.Begin);
                
                    var formatChecker = new FormatChecker();
                    expectedResults = formatChecker.GetExpectedValuesFromTestSet(ms);
                }                

                var fileSize = model.file.ContentLength/1024;
                var testSet = new TestSets
                {
                    CreatorId = context.Users.FirstOrDefault<Users>
                    (x => x.UserName.ToLower() == User.Identity.Name.ToLower()).Id,
                    TotalRuns = 0,
                    DateOfCreation = DateTime.Now,
                    Description = model.Description,
                    Data = array,
                    Name = model.TestName,
                    Size = fileSize,
                    ExpectedResults = expectedResults.ToString().TrimStart().TrimEnd()
                };

                context.TestSets.Add(testSet);

                context.SaveChanges();
            }

            return RedirectToAction("TestSets", "Testing");
        }

        public ActionResult TestDetails(string testName)
        {
            var result = new TestDescriptionViewModel();

            using (var context = new Entities())
            {
                var set = context.TestSets.Where(x => x.Name == testName).ToArray()[0];
                var lastTestRun = string.Empty;
                if (context.TestRuns.Where(x => x.TestSetId == set.Id).Count() != 0)
                {
                    lastTestRun = context.TestRuns.Where(x => x.TestSetId == set.Id).
                        OrderByDescending(p => p.DateOfRun).Take(1).ToList()
                        .Last().DateOfRun.ToString();
                }
                else
                {
                    lastTestRun = "never";
                }

                var fileSize = string.Empty;
                fileSize = set.Size == 0 ? "< 1" : set.Size.ToString();

                result = new TestDescriptionViewModel
                    {
                        CreatedBy = context.Users.FirstOrDefault<Users>(x => x.Id == set.CreatorId).UserName,
                        DateCreated = set.DateOfCreation.ToString(),
                        Description = set.Description,
                        FileSize = fileSize,
                        LastRun = lastTestRun,
                        Runs = context.TestRuns.Count(x => x.TestSetId == set.Id).ToString(),
                        TestName = set.Name
                    };
            }

            return View(result);
        }

        public ActionResult AlgDetails(string algName)
        {
            var result = new AlgorithmDescriptionViewModel();

            using (var context = new Entities())
            {
                var set = context.Algorithms.Where(x => x.Name == algName).ToArray()[0];
                var lastTestRun = string.Empty;
                if (context.TestRuns.Where(x => x.AlgorithmId == set.Id).Count() != 0)
                {
                    lastTestRun = context.TestRuns.Where(x => x.AlgorithmId == set.Id).
                        OrderByDescending(p => p.DateOfRun).Take(1).ToList()
                        .Last().DateOfRun.ToString();
                }
                else
                {
                    lastTestRun = "never";
                }

                result = new AlgorithmDescriptionViewModel
                {
                    CreatedBy = context.Users.FirstOrDefault<Users>(x => x.Id == set.CreatorId).UserName,
                    DateCreated = set.DateOfCreation.ToString(),
                    Description = set.Description,
                    LastRun = lastTestRun,
                    Runs = context.TestRuns.Count(x => x.AlgorithmId == set.Id).ToString(),
                    AlgorithmName = set.Name
                };
            }

            return View(result);
        }
    }
}