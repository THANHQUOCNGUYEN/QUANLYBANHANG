﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Demo_PhanQuyen
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

        public void Application_AuthenticateRequest(Object sender,EventArgs e)
        {
            var authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];

            if(authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var roles = authTicket.UserData.Split(new char[] { ',' });

                var userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);

                Context.User = userPrincipal;
            }
        }

        
    }
}
