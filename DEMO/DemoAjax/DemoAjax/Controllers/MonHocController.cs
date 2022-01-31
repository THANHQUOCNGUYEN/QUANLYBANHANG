using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoAjax.Models;
namespace DemoAjax.Controllers
{
    public class MonHocController : Controller
    {

        public QuanLyGiaoVien db = new QuanLyGiaoVien();
        // GET: MonHoc
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult danhsach()
        {
            var list = (from mh in db.MonHocs
                        select new
                        {
                            mamh = mh.MaMonHoc,
                            tenmh = mh.TenMonHoc
                        }).ToList();


            return Json(list, JsonRequestBehavior.AllowGet);
            
        }

    }
}