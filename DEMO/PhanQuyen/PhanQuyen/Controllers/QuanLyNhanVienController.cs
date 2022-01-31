using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhanQuyen.Controllers
{
    [Authorize(Roles = "QLNV")]
    public class QuanLyNhanVienController : Controller
    {
        // GET: QuanLyNhanVien

        public ActionResult Index()
        {
            return View();
        }
    }
}