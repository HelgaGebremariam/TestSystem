﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace TestSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //WebSecurity.InitializeDatabaseConnection("Entities", "Users", "Id", "UserName");
            //if (!System.Web.Security.Roles.RoleExists("Admins"))
            //{
            //    System.Web.Security.Roles.CreateRole("Admins");
            //}            
        }

        protected void Application_AcquireRequestState(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                var ci = (CultureInfo)this.Session["Culture"];

                if (ci == null)
                {
                    ci = new CultureInfo("en");
                    this.Session["Culture"] = ci;
                }

                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
        }
    }


}
