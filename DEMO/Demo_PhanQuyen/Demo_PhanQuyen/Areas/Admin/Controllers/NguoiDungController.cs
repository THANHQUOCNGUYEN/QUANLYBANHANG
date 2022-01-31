using Demo_PhanQuyen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Demo_PhanQuyen.Areas.Admin.Controllers
{
    public class NguoiDungController : Controller
    {
        private DataModel_PhanQuyen db = new DataModel_PhanQuyen();

        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string userName = f["txtUserName"].ToString();
            string password = f["txtpassword"].ToString();
            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.username == userName && n.password == password);

            //truy cap lay ra tat ca cac quyen cua thanh vien do
            if (tv != null)
            {
                var listQuyen = db.LoaiTV_Quyen.Where(s => s.maloaiTV == tv.maloaiTV);
                string Quyen = "";
                Session["User"] = tv.username;
                foreach (var item in listQuyen)
                {
                    Quyen += item.Quyen.MaQuyen + ",";
                }

                //nếu quyền khách null thì
                if (Quyen != "")
                {
                    Quyen = Quyen.Substring(0, Quyen.Length - 1);//Cắt đi dấu phẩy cuối;
                    //Phân Quyền Cho Người Dùng
                    PhanQuyen(tv.username, Quyen);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.error = "Tài khoản hoặc mật khẩu không đúng !";
            }
            return View();
        }

        //Tao trang ngan chan quyen truy cap
        public ActionResult LoiPhanQuyen()
        {
            return View();
        }
        public ActionResult Logout()
        {
            //Huy session
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "NguoiDung");
        }
        public void PhanQuyen(string userName, string quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
                                            userName,
                                            DateTime.Now,
                                            DateTime.Now.AddHours(3),
                                            false,
                                            quyen,
                                            FormsAuthentication.FormsCookiePath);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

            Response.Cookies.Add(cookie);
        }
    }
}