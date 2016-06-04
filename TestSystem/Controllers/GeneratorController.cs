using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSystem.Models;
using TestSystem.TestGenerator;
using TestSystem.TestGenerator.Interfaces;

namespace TestSystem.Controllers
{
    public class GeneratorController : Controller
    {
        // GET: Generator
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
    }
}