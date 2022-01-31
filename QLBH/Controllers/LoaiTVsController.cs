using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLBH.Models;

namespace QLBH.Controllers
{
    public class LoaiTVsController : Controller
    {
        private QuanLyBanHang db = new QuanLyBanHang();

        // GET: LoaiTVs
        public ActionResult Index()
        {
            return View(db.LoaiTVs.ToList());
        }

        // GET: LoaiTVs/Details/5
        public ActionResult PhanQuyen(int? id)
        {
            LoaiTV loaitv = db.LoaiTVs.Find(id);
            //danh sach quyen
            ViewBag.MaQuyen = db.Quyens;

            //danh sach quyen cua loai thanh vien do
            ViewBag.LoaiTVQuyen = db.LoaiTV_Quyen.Where(s => s.MaLoaiTV == id);

            return View(loaitv);
        }

        [HttpPost]
        public ActionResult PhanQuyen(int? MaLTV, IEnumerable<LoaiTV_Quyen> lstPhanQuyen)
        {
            //trường hợp nếu đã tiến hành phân quyền rồi nhưng muốn phân quyền lại

            //xoa nhung quyen thuoc loai thanh vien do
            var lstDaPhanQuyen = db.LoaiTV_Quyen.Where(n => n.MaLoaiTV == MaLTV);
            if (lstDaPhanQuyen != null)
            {
                db.LoaiTV_Quyen.RemoveRange(lstDaPhanQuyen);
                db.SaveChanges();
            }

            //Kiem tra list danh sach quyen duoc check

            if (lstPhanQuyen != null)
            {
                foreach (var item in lstPhanQuyen)
                {
                    if (item.MaQuyen != null)
                    {
                        item.MaLoaiTV = (int)MaLTV;
                        //Neu duoc check thì Insert vào bảng table
                        db.LoaiTV_Quyen.Add(item);
                    }
                }
                db.SaveChanges();

            }
            TempData["PhanQuyen"] = "Phân Quyền Thành Công !";
            return RedirectToAction("Index");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
