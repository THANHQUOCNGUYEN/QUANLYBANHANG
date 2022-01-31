using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace QLBH
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.DefaultNamespaces.Add("QLBH.Controllers");

            //Application["PageView"] = 0;//Đếm số lượng người đang truy cập.
            Application["SonguoiDangOnline"] = 0;//Đếm số lượng người đang online


        }

        protected void Session_Start()
        {
            Application.Lock();// dung de dong bo hoa
            //Application["PageView"] = (int)Application["PageView"] + 1;
            Application["SonguoiDangOnline"] = (int)Application["SonguoiDangOnline"] + 1;
            Application.UnLock();

            int count_visit = 0;
            // Đọc dử liều từ file count_visit.txt
            System.IO.StreamReader read = new System.IO.StreamReader(Server.MapPath("~/visit.txt"));
            count_visit = int.Parse(read.ReadLine());
            read.Close();
            // Tăng biến count_visit thêm 1
            count_visit++;

            Application.Lock();
            Application["PageView"] = count_visit;
            Application.UnLock();

            // Lưu dử liệu vào file count_visit.txt
            System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("~/visit.txt"));
            writer.WriteLine(count_visit);
            writer.Close();
        }
        protected void Session_End()
        {
            Application.Lock();// dung de dong bo hoa
            Application["SonguoiDangOnline"] = (int)Application["SonguoiDangOnline"] - 1;
            Application.UnLock();
        }

        protected void Application_End()
        {
            Application.Lock();// dung de dong bo hoa
            Application["SonguoiDangOnline"] = (int)Application["SonguoiDangOnline"] - 1;
            Application.UnLock();
        }

    }
}
