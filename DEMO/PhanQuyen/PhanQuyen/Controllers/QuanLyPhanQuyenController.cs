using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhanQuyen.Models;
namespace PhanQuyen.Controllers
{
    [Authorize(Roles = "PHANQUYEN")]
    public class QuanLyPhanQuyenController : Controller
    {
        public ModelPhanQuyen db = new ModelPhanQuyen();
        // GET: QuanLyPhanQuyen
        public ActionResult Index()
        {
            if (TempData["PhanQuyen"] != null)
            {
                ViewBag.ThanhCong = TempData["PhanQuyen"];
            }
            var list = db.UserGroups.ToList();
            return View(list);
        }

        public ActionResult PhanQuyen(string id)
        {
            UserGroup loaitv = db.UserGroups.Find(id);
            //danh sach quyen
            ViewBag.MaQuyen = db.Quyens;

            //danh sach quyen cua loai thanh vien do
            ViewBag.LoaiTVQuyen = db.LoaiTV_Quyen.Where(s => s.maloaitv == id);

            return View(loaitv);
        }

        [HttpPost]
        public ActionResult PhanQuyen(string MaLTV, IEnumerable<LoaiTV_Quyen> lstPhanQuyen)
        {
            //trường hợp nếu đã tiến hành phân quyền rồi nhưng muốn phân quyền lại

            //xoa nhung quyen thuoc loai thanh vien do
            var lstDaPhanQuyen = db.LoaiTV_Quyen.Where(n => n.maloaitv == MaLTV);
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
                    if (item.maquyen != null)
                    {
                        item.maloaitv = MaLTV;
                        //Neu duoc check thì Insert vào bảng table
                        db.LoaiTV_Quyen.Add(item);
                    }
                }
                db.SaveChanges();

            }
            TempData["PhanQuyen"] = "Phân Quyền Thành Công !";
            return RedirectToAction("Index");   
        }

        [HttpPost]
        public JsonResult ThemNhom(string maloai,string tenloai)
        {
            try
            {
                UserGroup us = new UserGroup();
                us.maloai = maloai;
                us.tenloai = tenloai;

                db.UserGroups.Add(us);
                db.SaveChanges();
                return Json(new {code = 200, msg="Them thanh cong !"},JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Them That bai !"+ex }, JsonRequestBehavior.AllowGet);

            }


        }
        public JsonResult Xoa(string id)
        {
            try
            {
                var thanhvien = db.UserGroups.Find(id);
                var loaitv_quyen = db.LoaiTV_Quyen.Where(s => s.maloaitv == id);
                db.LoaiTV_Quyen.RemoveRange(loaitv_quyen);
                db.UserGroups.Remove(thanhvien);
                db.SaveChanges();

                return Json(new { code = 200, msg = "Xoa thanh cong" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { code = 500, msg = "Xoa that bai" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CapNhat(string id,string tenloai)
        {
            try
            {
                var thanhvien = db.UserGroups.Find(id);
                thanhvien.tenloai = tenloai;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Sua thanh cong !" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { code = 500, msg = "Sua that bai !" }, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult LayDanhsach()
        {
            var lst = (from l in db.UserGroups
                       select new
                       {
                           maloai = l.maloai,
                           tenloai = l.tenloai
                       }).ToList();

            return Json(lst, JsonRequestBehavior.AllowGet);
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