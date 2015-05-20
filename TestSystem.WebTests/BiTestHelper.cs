﻿using Lab02_MultilayerPerceptron;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.WebTests
{


    static class BiTestHelper
    {
        public static VectorsContainer GetVectorContainer(String pathA, String pathB, String pathC, NeuronHelper neuronHelper)
        {
            var vectorA = neuronHelper.ConvertToVector(new PictureContainer(pathA, 10));
            var vectorB = neuronHelper.ConvertToVector(new PictureContainer(pathB, 10));
            var vectorC = neuronHelper.ConvertToVector(new PictureContainer(pathC, 10));

            return new VectorsContainer(vectorA, vectorB, vectorC);
        }

        public static string RunAlgorithmAnalyze(string testsPath, string outPath, float alpha)
        {
            var testingResults = string.Empty;
            var neuronHelper = new NeuronHelper();
            var expectedResults = string.Empty;
            var container = BiTestHelper.GetVectorContainer(PicturesPath.PathToOriginalA, PicturesPath.PathToOriginalB,
                PicturesPath.PathToOriginalC, new NeuronHelper());
            var error = 0.05;
            var beta = 0.01;
            var timeout = 10000;

            var perceptron = new Perceptron(
               alpha,
               beta,
               error,
               timeout,
               container);

            var helper = new PerceptronHelper(perceptron, 100, 3);

            helper.Teach();

            using (StreamReader file = new System.IO.StreamReader(testsPath, true))
            {

                while (!file.EndOfStream)
                {
                    var testLine = file.ReadLine();
                    expectedResults += testLine.ToCharArray()[0] + " ";
                    testLine = testLine.Remove(0, 2);
                    var byteVector = neuronHelper.ConvertTestInputToVector(testLine);

                    var percentage = helper.Recognize(byteVector);

                    var classNumber = Array.IndexOf<double>(percentage, percentage.Max()) + 1;

                    testingResults += classNumber.ToString() + " ";
                }
            }

            using (StreamWriter file = new System.IO.StreamWriter(@"D:\DPtests\results.txt", true))
            {
                file.WriteLine(testingResults);
            }
            
            return testingResults;
        }


    }

    //[TestMethod]
        
    //    public void TestNTimesAndCalcMiddle() 
    //    {
    //        //30 tests for 1 testSuite that contains 20 test inputs with different learning. output is their middle true positive rate 
    //        //for 0, 10, 20, 30, 40 percentage of noise

    //        var outputPath = @"D:\DPtests\comparsionResultsCommon.txt";
    //        //var outputPath1 = @"D:\DPtests\comparsionResults20PercNoise.txt";
    //        //var outputPath2 = @"D:\DPtests\comparsionResults30PercNoise.txt";
    //        //var outputPath3 = @"D:\DPtests\comparsionResults10PercNoise.txt";
    //        //var outputPath4 = @"D:\DPtests\comparsionResults40PercNoise.txt";
    //        //var outputPath5 = @"D:\DPtests\comparsionResults0PercNoise.txt";
    //        var testPath1 = @"D:\DPtests\test0percNoise.txt";
    //        var testPath2 = @"D:\DPtests\test10percNoise.txt";
    //        var testPath3 = @"D:\DPtests\test20percNoise.txt";
    //        var testPath4 = @"D:\DPtests\test30percNoise.txt";
    //        var testPath5 = @"D:\DPtests\test40percNoise.txt";
    //        int numberOfRepeats = 30;

    //        RunTestingNTimesWithStat(testPath1, outputPath, numberOfRepeats);
    //        RunTestingNTimesWithStat(testPath2, outputPath, numberOfRepeats);
    //        RunTestingNTimesWithStat(testPath3, outputPath, numberOfRepeats);
    //        RunTestingNTimesWithStat(testPath4, outputPath, numberOfRepeats);
    //        RunTestingNTimesWithStat(testPath5, outputPath, numberOfRepeats);
    //    }

    //    [TestMethod]
    //    public void RunAlphaValueVariationTests()
    //    {
    //        var outputPath = @"D:\DPtests\alphaChangeTestingResults.txt";
    //        var testPath20percent = @"D:\DPtests\test20percNoise.txt";
    //        var rocCurveCreator = new TestSystem.Core.RocCurveCreator();
    //        var testingResults = new List<string>();
    //        TestFormatConverter formatConverter = new TestFormatConverter();

    //        var expectedResults = formatConverter.GetResultsFromTestSet(testPath20percent);

    //        for (var i = 0f; i < 1; i += 0.05f)
    //        {
    //            testingResults.Add(BiTestHelper.RunAlgorithmAnalyze(testPath20percent, outputPath, i));
    //        }


    //        var result =  rocCurveCreator.GenerateRocCurveCoordinates(expectedResults, testingResults, "2");
    //        var resultStr = string.Empty;

    //        for (var i = 0; i < result.rocCoordinatesSensivity.Count; i++)
    //        {
    //            resultStr += "[" + result.rocCoordinatesSensivity[i] + ", " + result.rocCoordinatesSpecifity[i] + "],";
    //        }

    //            using (StreamWriter file = new System.IO.StreamWriter(outputPath, true))
    //            {
    //                file.WriteLine(resultStr);
    //            }
    //    }
        
    //    private void RunTestingNTimesWithStat(string testPath, string outputPath, int numberOfRepeats)
    //    {
    //        var percents = new List<float>();

    //        //for (var i = 0; i < numberOfRepeats; i++)
    //        //{
    //        //    percents.Add(BiTestHelper.RunAlgorithmAnalyze(testPath, outputPath, 0.7f));
    //        //}

    //        float count = percents.Count;
    //        float middle = percents.Sum() / count;
    //        percents.Clear();

    //        using (StreamWriter file = new System.IO.StreamWriter(outputPath, true))
    //        {
    //            file.WriteLine(DateTime.Now + ": middle true pos - " + middle.ToString("0.00"));
    //        }
    //    }

          

}
