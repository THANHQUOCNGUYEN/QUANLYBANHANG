using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhanTrang.Models;
namespace PhanTrang.Controllers
{
    public class HomeController : Controller
    {
        public Model1 db = new Model1();
        public ActionResult Index(int? page,int? maloai)
        {
            
            if(maloai == null)
            {
                if (page == null)
                {
                    page = 1;
                }
                var list = db.SanPhams.ToList();
                int pageCurrent = (int)page;
                ViewBag.pageCurrent = pageCurrent;

                int tongtrang = list.Count();
                int pagesize = 1;
                int numberPage = (tongtrang % pagesize) == 0 ? tongtrang / pagesize : tongtrang / pagesize + 1;
                ViewBag.number = numberPage;
                var lst = list.OrderBy(s => s.MaSP).Skip((int)(page - 1) * pagesize).Take(pagesize);
                return View(lst);
            }
        else
            {
                if (page == null)
                {
                    page = 1;
                }
                var list = db.SanPhams.Where(s=>s.MaLoaiSP==maloai).ToList();
                int pageCurrent = (int)page;
                ViewBag.pageCurrent = pageCurrent;
                ViewBag.maloai = maloai;
                int tongtrang = list.Count();
                int pagesize = 1;
                int numberPage = (tongtrang % pagesize) == 0 ? tongtrang / pagesize : tongtrang / pagesize + 1;
                ViewBag.number = numberPage;
                var lst = list.OrderBy(s => s.MaSP).Skip((int)(page - 1) * pagesize).Take(pagesize);
                return View(lst);
            }
          
        }

        public ActionResult danhmuc()
        {
            var lst = db.LoaiSPs.ToList();
            return PartialView(lst);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}