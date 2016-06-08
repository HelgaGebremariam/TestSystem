using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TestSystem.Models;
using TestSystem.TestGenerator;
using TestSystem.TestGenerator.Interfaces;

namespace TestSystem.Controllers
{
    public class GeneratorController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView("SequenceStatistics");

            return View();
        }

        [HttpPost]
        public ActionResult Index(GeneratorParamentersViewModel model)
        {
            return View();
        }

        public PartialViewResult SequenceStatistics()
        {
            SequenceStatisticsViewModel model = new SequenceStatisticsViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public PartialViewResult SequenceStatistics(GeneratorParamentersViewModel model)
        {
            SequenceStatisticsViewModel sequenceStatsticsModel = new SequenceStatisticsViewModel();
            return PartialView();
        }
        public ActionResult SequenceStatisticsChart(SequenceStatisticsViewModel model)
        {
            IRandomGenerator randomGenerator = new MushGenerator();
            ISequenceGenerator sequenceGenerator = new SequenceGenerator(randomGenerator);
            StatisticsCreator statisticsCreator = new StatisticsCreator(sequenceGenerator);
            sequenceGenerator.Min = 0;
            sequenceGenerator.Max = 1;
            sequenceGenerator.RoofMeanSquareDeviation = 0.1;
            int[] gistogramm = statisticsCreator.CreateGistogramm(30);
            string[] xValues = new string[30];
            for (int i = 0; i < 30; i++)
                xValues[i] = " ";
            model.Gistogramm = new Chart(500, 400, ChartTheme.Blue);
            model.Gistogramm.AddSeries(xValue: xValues,
    yValues: gistogramm);
            return PartialView(model);
        }
    }
}