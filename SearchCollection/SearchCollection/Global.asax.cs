using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SearchCollection
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
        }

        //public void Application_PreRequestHandlerExecute(Object source, EventArgs e)
        //{
        //    HttpApplication application = (HttpApplication)source;
        //    HttpContext context = application.Context;

        //    // use an if statement to make sure the request is not for a static file (js/css/html etc.)
        //    if (context != null && context.Session != null)
        //    {
        //        // use context to work the session
        //        if (Session["SRUser"] == null)
        //        {
        //            Session["SRUser"] = null;
        //        }
        //    }
        //}

        public void Session_End(Object sender, EventArgs E)
        {
            Session.RemoveAll();
            Session.Clear();
        }
    }
}
