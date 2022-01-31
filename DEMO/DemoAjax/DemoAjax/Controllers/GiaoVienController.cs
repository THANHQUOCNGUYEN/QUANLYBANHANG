using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoAjax.Models;
namespace DemoAjax.Controllers
{
    public class GiaoVienController : Controller
    {
        public QuanLyGiaoVien db = new QuanLyGiaoVien();

        // GET: GiaoVien
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LayDanhSachGV(int trang,string tukhoa)
        {
            var list = (from gv in db.GiaoViens
                        join mh in db.MonHocs
                        on gv.MaMonHoc equals mh.MaMonHoc
                        select new
                        {
                            magv = gv.MaGV,
                            tengv = gv.TenGV,
                            ngsinh = gv.NgSinh.Value.ToString(),

                            gioitinh = gv.GioiTinh,
                            mamh =  gv.MaMonHoc,
                            tenmh = mh.TenMonHoc
                        }).Where(s=>s.tengv.Contains(tukhoa)).ToList();

            int pageSize = 3;

            var lsht = list.Skip((trang - 1) * pageSize).Take(pageSize);

            int sotrang = list.Count % pageSize == 0 ? list.Count / pageSize : list.Count / pageSize + 1;

            return Json(new { list = lsht,sotrang = sotrang }, JsonRequestBehavior.AllowGet);
        }

       public JsonResult ThemGV(string tengv,DateTime ngsinh,bool gioitinh,int mamon)
        {
            try
            {
                GiaoVien gv = new GiaoVien();
                gv.TenGV = tengv;
                gv.NgSinh = ngsinh;
                gv.GioiTinh = gioitinh;

                gv.MaMonHoc = mamon;

                db.GiaoViens.Add(gv);
                db.SaveChanges();

                return Json(new { code =200, msg = "Thêm thành công !" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm thất bại ! " }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CapNhatGiaoVien(int magv,string tengv, DateTime ngsinh, bool gioitinh, int mamon)
        {
            try
            {
                var giaovien = db.GiaoViens.Find(magv);

                giaovien.TenGV = tengv;
                giaovien.NgSinh = ngsinh;
                giaovien.GioiTinh = gioitinh;
                giaovien.MaMonHoc = mamon;

                db.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật thành công !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Câp nhật thất bại ! " }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult XoaGiaoVien(int magv)
        {
            try
            {
                var giaovien = db.GiaoViens.Find(magv);

                db.GiaoViens.Remove(giaovien);
                db.SaveChanges();

                return Json(new { code = 200, msg = "giáo viên đã dăng" },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "chua dang" },JsonRequestBehavior.AllowGet);
            }
        }
    }
}