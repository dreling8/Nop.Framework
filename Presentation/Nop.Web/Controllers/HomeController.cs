using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core.Infrastructure;
using Nop.Domain.Logging;
using Nop.Domain.Users;
using Nop.Services.Logging; 

namespace Nop.Web.Controllers
{
    public class HomeController : Controller
    { 
        public ILogger _logger;
        public IUserActivityService _userActivityService;

        public HomeController( 
          ILogger logger,
          IUserActivityService userActivityService
          )
        { 
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
            var user = new User()
            { 
                Username = "adfsdf",
                UserGuid = Guid.NewGuid(),
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow,
            };

            _logger.InsertLog(LogLevel.Information, "index visit");
            _userActivityService.InsertActivity(ActivityLogTypeEnum.AddUser, "添加用户{0},{1}", new string[2] { "aaaa", "bbb" }); 
        }

    }
}