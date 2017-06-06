using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Nop.Core.Infrastructure;
using Nop.Services.Logging;

namespace Nop.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //引擎上下文初始化
            EngineContext.Initialize(false);


            //log application start
            try
            {
                //log
                var logger = EngineContext.Current.Resolve<ILogger>();
                logger.Information("Application started", null, null);
            }
            catch (Exception)
            {
                //don't throw new exception if occurs
            }
        }

         
    }
}
