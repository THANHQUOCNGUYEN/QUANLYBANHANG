using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Ajax_Demo.Models;
namespace Demo_Ajax2.Controllers
{
    public class LopHocController : Controller
    {
        // GET: LopHoc
        public ModeHocSinh context = new ModeHocSinh();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DSLop(string TuKhoa, int trang)
        {
            try
            {
                int pagesize = 3;
                //dua tu khoa tim kiem ve dang binh thuog//
                var dsLop = context.LopHocs.Where(s => s.tenlop.Contains(TuKhoa)).ToList();
                //var dsLop = context.LopHocs.Where(s => s.tenlop.Contains(Tukhoa)).ToList();

                //chia lấy phần nguyên và phần dư

                var kqht = dsLop.Skip((trang - 1) * pagesize).Take(pagesize).ToList();

                var sotrang = dsLop.Count % pagesize == 0 ? dsLop.Count / pagesize : dsLop.Count / pagesize + 1;
                return Json(new { code = 200, dsLop = kqht, soTrang = sotrang, msg = "Lay danh sach lop thanh cong !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult AddLop(string tenlop, string mota)
        {
            try
            {
                var lop = new LopHoc();
                lop.tenlop = tenlop;
                lop.mota = mota;

                context.LopHocs.Add(lop);
                context.SaveChanges();

                return Json(new { code = 200, msg = "Them thanh cong !" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { code = 500, msg = "Them that bai", JsonRequestBehavior.AllowGet });
            }
        }

        [HttpGet]

        public JsonResult XemChiTiet(int id)
        {
            try
            {
                var lop = context.LopHocs.Find(id);

                return Json(new { code = 200, l = lop, msg = "LAY THONG TIN CHI TIET CUA LOP THANH CONG !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lay thong tin lop that bai " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CapNhat(int id, string tenlop1, string mota1)
        {
            try
            {
                //tim lop dua vao id
                var l = context.LopHocs.Find(id);
                l.tenlop = tenlop1;
                l.mota = mota1;

                context.SaveChanges();
                return Json(new { code = 200, msg = "Cập nhật lớp thành công !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật không thất bại ! " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult XoaLop(int id)
        {
            try
            {
                var l = context.LopHocs.Find(id);
                //chỗ này ta cập nhật lại trạng thái !
                context.LopHocs.Remove(l);
                context.SaveChanges();

                return Json(new { code = 200, msg = "Xóa lớp học thành công !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa lớp học thất bại !" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}