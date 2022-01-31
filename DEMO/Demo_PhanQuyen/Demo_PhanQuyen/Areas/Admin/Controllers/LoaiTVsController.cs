using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Demo_PhanQuyen.Models;

namespace Demo_PhanQuyen.Areas.Admin.Controllers
{
    [Authorize(Roles ="QUANTRI")]
    public class LoaiTVsController : Controller
    {
        DataModel_PhanQuyen db = new DataModel_PhanQuyen();

        public ActionResult Index()
        {
            
            return View(db.LoaiTVs);
        }
        public ActionResult PhanQuyen(int? id)
        {
            LoaiTV loaitv = db.LoaiTVs.Find(id);
            //danh sach quyen
            ViewBag.MaQuyen = db.Quyens;

            //danh sach quyen cua loai thanh vien do
            ViewBag.LoaiTVQuyen = db.LoaiTV_Quyen.Where(s => s.maloaiTV == id);

            return View(loaitv);
        }

        [HttpPost]
        public ActionResult PhanQuyen(int? MaLTV,IEnumerable<LoaiTV_Quyen> lstPhanQuyen)
        {
            //trường hợp nếu đã tiến hành phân quyền rồi nhưng muốn phân quyền lại
            
            //xoa nhung quyen thuoc loai thanh vien do
            var lstDaPhanQuyen = db.LoaiTV_Quyen.Where(n => n.maloaiTV == MaLTV);
            if(lstDaPhanQuyen != null)
            {
                db.LoaiTV_Quyen.RemoveRange(lstDaPhanQuyen);
                db.SaveChanges();
            }

            //Kiem tra list danh sach quyen duoc check

            if(lstPhanQuyen != null)
            {
                foreach (var item in lstPhanQuyen)
                {
                    if (item.maquyen != null)
                    {
                        item.maloaiTV = (int)MaLTV;
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
