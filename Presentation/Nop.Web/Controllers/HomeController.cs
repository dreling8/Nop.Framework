using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core.Infrastructure;
using Nop.Domain.Common;
using Nop.Domain.Logging;
using Nop.Domain.Users;
using Nop.Services.Logging; 

namespace Nop.Web.Controllers
{
    public class HomeController : Controller
    { 
        public ILogger _logger;
        public IUserActivityService _userActivityService;
        public CommonSettings _commonSettings;
        public HomeController( 
          ILogger logger,
          IUserActivityService userActivityService,
          CommonSettings commonSettings
          )
        { 
            _logger = logger;
            _userActivityService = userActivityService;
            _commonSettings = commonSettings;
        }

        public ActionResult Index()
        {
            TestSettings();
            TestLogger();
            return View();
        }


        private void TestSettings()
        {
            var s = _commonSettings.IgnoreLogWordlist;
        }

        private void TestLogger()
        { 
            _logger.InsertLog(LogLevel.Information, "index visit");
            _userActivityService.InsertActivity(ActivityLogTypeEnum.AddUser, "添加用户{0},{1}", new string[2] { "aaaa", "bbb" }); 
        }

    }
}