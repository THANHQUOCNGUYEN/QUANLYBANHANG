﻿using System.Web;
using System.Web.Mvc;

namespace Demo_PhanQuyen
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
