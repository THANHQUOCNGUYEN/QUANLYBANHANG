using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhanQuyen.Controllers
{
    [Authorize(Roles = "QLKH")]
    public class QuanLyKhachHangController : Controller
    {
       
        // GET: QuanLyKhachHang
        public ActionResult Index()
        {
            return View();
        }
    }
}