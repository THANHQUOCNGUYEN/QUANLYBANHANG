using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhanQuyen.Controllers
{
    //Gán các quyền cho nó
    [Authorize(Roles = "QLHD")]
    public class QuanLyHoaDonController : Controller
    {
        // GET: QuanLyHoaDon
        public ActionResult Index()
        {
            return View();
        }
    }
}