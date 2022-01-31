using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using CaptchaMvc.HtmlHelpers;
using System.Web.Security;
using QLBH.Models;
namespace QLBH.Controllers
{
    public class NguoiDungController : Controller
    {
        QuanLyBanHang db = new QuanLyBanHang();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }

        //Hàm phân quyền

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(User us)
        {
            var dao = new UserDAO();
            var md5 = dao.MD5Hash(us.Password);
            var item = db.Users.Where(s => s.Email.Equals(us.Email) && s.Password.Equals(md5)).FirstOrDefault();

            if (item == null)
            {
                ViewBag.error = "Kiểm Tra Lại Email Hoặc Password !";
            }
            else
            {
                //Nếu đăng nhập thành công thì lưu section
                Session["Email"] = item.Email;
                Session["ID"] = item.IDUser;

                //Lấy tất cả các quyền của thành viên đó

                //Chuyển đến trang admin
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        

        [HttpGet]
        public ActionResult DangKi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKi(User us)
        {
            //Quan trong la cho nay de cho no valid form
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                if (dao.checktendangnhap(us.UserName))
                {
                    ViewBag.username = "Tên đăng nhập đã tồn tại";
                }
                else if (dao.checkemail(us.Email))
                {
                    ViewBag.email = "Email đã tồn tại !";
                }
                else
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    us.Password = dao.MD5Hash(us.Password);

                    us.LoaiTV = 4;//Loai thanh vien nguoi dung la thuong
                    if (!this.IsCaptchaValid(""))
                    {
                        ViewBag.er = "Vui lòng nhập lại Captcha !";
                    }
                    else
                    {

                        db.Users.Add(us);
                        db.SaveChanges();

                        TempData["ThanhCong"] = "Đăng kí thành công !";

                        return RedirectToAction("Index", "Home");
                        //return RedirectToAction("DangNhap", "NguoiDung");
                    }
                }

            }

            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            //Session["Email"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult gis()
        {
            return View();
        }


        public ActionResult LoiPhanQuyen()
        {
            return View();
        }

    }
}