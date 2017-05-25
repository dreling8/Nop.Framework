using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Domain.Test;
using Nop.Services.Tests;

namespace Nop.Web.Controllers
{
    public class HomeController : Controller
    {
        public ITestService _testService;

        public HomeController(
          ITestService testService
          )
        {
            _testService = testService;
        }

        public ActionResult Index()
        {
            var entity = new TestEntity()
            {
                CreateDate = DateTime.Now,
                Description = "描述2",
                Name = "测试数据2"
            };
            _testService.InsertTest(entity);

            var tests = _testService.GetAllTests();

            return View();
        }

    }
}