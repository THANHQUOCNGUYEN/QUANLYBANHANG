using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhanQuyen.Controllers
{
    [Authorize(Roles = "QLSP")]
    public class QuanLySanPhamController : Controller
    {
        // GET: QuanLySanPham
        public ActionResult Index()
        {
            return View();
        }
    }
}