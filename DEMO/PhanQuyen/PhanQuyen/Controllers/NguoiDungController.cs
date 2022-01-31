using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PhanQuyen.Models;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Security.Cryptography;

namespace PhanQuyen.Controllers
{
    public class NguoiDungController : Controller
    {
        public ModelPhanQuyen db = new ModelPhanQuyen();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            if(Request.Cookies["TenDangNhap"] != null && Request.Cookies["UserPassword"] != null)
            {
                ViewBag.tendangnhap = Request.Cookies["TenDangNhap"].Value;
                ViewBag.matkhau = Request.Cookies["UserPassword"].Value;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string username = f["txtusername"].ToString();
            string password = f["txtpassword"].ToString();

            //Ma hoa md5 phuong Login
            User tv = db.Users.FirstOrDefault(s => s.tendangnhap == username && s.matkhau == password);

            //dang nhap thanh cong
            if (tv != null)
            {
                if (f["remember"] == "true")
                {
                    Response.Cookies["TenDangNhap"].Value = tv.tendangnhap;
                    Response.Cookies["UserPassword"].Value = tv.matkhau;
                    Response.Cookies["TenDangNhap"].Expires = DateTime.Now.AddMinutes(1);//cookies het han trong 1 phut
                    Response.Cookies["UserPassword"].Expires = DateTime.Now.AddMinutes(1);

                }
                else
                {
                    Response.Cookies["TenDangNhap"].Expires = DateTime.Now.AddMinutes(-1);
                    Response.Cookies["UserPassword"].Expires = DateTime.Now.AddMinutes(-1);
                }
                var listQuyen = db.LoaiTV_Quyen.Where(s => s.maloaitv == tv.maloaitv);
                string Quyen = "";
                Session["User"] = tv.tendangnhap;
                foreach (var item in listQuyen)
                {
                    Quyen += item.Quyen.maquyen + ",";
                }

                //nếu quyền khách null thì
                if (Quyen != "")
                {
                    Quyen = Quyen.Substring(0, Quyen.Length - 1);//Cắt đi dấu phẩy cuối;
                    //Phân Quyền Cho Người Dùng
                    PhanQuyen(tv.tendangnhap, Quyen);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.error = "Tài khoản hoặc mật khẩu không đúng !";
            }
            return View();
        }

        public ActionResult ForgerPassword()
        { 
            return View();
        }       
  
        [HttpPost]
        public ActionResult ForgerPassword(string email)
        {
            var us = db.Users.Where(s => s.tendangnhap == "USER001").FirstOrDefault();

            //Xet dieu kien neu ton tai email moi gui pass nguoc lai khong gui pass
            MailMessage ma = new MailMessage("ngtquoc163@gmail.com",email);
            ma.Subject = "Your Password";
            ma.Body = string.Format("Hello :<h1>{0}</h1><br/>your password is <h1>{1}</h1>", us.tendangnhap,us.matkhau);
            ma.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential();
            nc.UserName = "ngtquoc163@gmail.com";
            nc.Password = "ntq955706";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Port = 587;
            smtp.Send(ma);

            ViewBag.thanhcong = "Password cua ban da duoc gui den dia chi email !";

            return View();
        }
        //hàm phân quyền cho người dùng
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

        //Tao trang ngan chan quyen truy cap
        public ActionResult LoiPhanQuyen()
        {
            return PartialView();
        }

        //ham logout
        public ActionResult Logout()
        {
            //Huy session
            
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "NguoiDung");
        }
    }
}