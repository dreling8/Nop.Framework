using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core.Infrastructure;
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
        public IUserActivityService _userActivityService;

        public HomeController(
          ITestService testService,
          ILogger logger,
          IUserActivityService userActivityService
          )
        {
            _testService = testService;
            _logger = logger;
            _userActivityService = userActivityService;
        }

        public ActionResult Index()
        {
            TestCommonMoudle();
            return View();
        }


        private void TestCommonMoudle()
        {
            _logger.InsertLog(LogLevel.Information, "index visit");
            _userActivityService.InsertActivity("AddUser", "添加用户{0},{1}", new string[] { "aaaa", "bbb" });
        }

    }
}