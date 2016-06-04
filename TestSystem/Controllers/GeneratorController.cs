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
            return View();
        }

        [HttpPost]
        public ActionResult Index(GeneratorParamentersViewModel model)
        {
            return View();
        }

        public ActionResult SequenceStatistics()
        {
            SequenceStatisticsViewModel model = new SequenceStatisticsViewModel();
            string[] xValues = new string[5];
            for (int i = 0; i < 5; i++)
                xValues[i] = " ";
            model.Gistogramm = new Chart(500, 100);
            model.Gistogramm.AddTitle("Distribution Gistogramm");
            model.Gistogramm.AddSeries(xValue: xValues,
                yValues: new int[] {2, 6, 4, 5, 3});
            return View(model);
        }
    }
}