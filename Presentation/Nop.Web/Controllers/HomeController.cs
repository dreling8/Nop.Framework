using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Domain.Logging;
using Nop.Domain.Test;
using Nop.Services.Logging;
using Nop.Services.Tests;

namespace Nop.Web.Controllers
{
    public class HomeController : Controller
    {
        public ITestService _testService;

        public ILogger _logger;

        public HomeController(
          ITestService testService,
          ILogger logger
          )
        {
            _testService = testService;
            _logger = logger;
        }

        public ActionResult Index()
        {
            _logger.InsertLog(LogLevel.Debug, "test");

            return View();
        }

    }
}