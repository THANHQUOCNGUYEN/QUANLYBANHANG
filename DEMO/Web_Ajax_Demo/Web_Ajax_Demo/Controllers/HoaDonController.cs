using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Ajax_Demo.Models;
namespace Web_Ajax_Demo.Controllers
{
    public class HoaDonController : Controller
    {
        // GET: HoaDon
        public ModeHocSinh db = new ModeHocSinh();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadHoaDon()
        {
            try
            {
               
                var lst = (from hd in db.HoaDons 
                           join kh in db.KhachHangs
                           on hd.MaKH equals kh.MaKH
                           select new
                           {
                               mahd = hd.MaHD,
                               makh = kh.MaKH,
                               tenkh = kh.TenKH,
                               //ep kieu ve dang chuoi
                               nglap = hd.NgayLap.Value.ToString(),
                               mota = hd.Mota
                              
                           }).ToList();

              
                return Json(new { code = 200, dshoadon = lst, msg = "load thanh cong !" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "load that bai" + ex.Message}, JsonRequestBehavior.AllowGet);
            }
           
        }
        [HttpPost]
        public JsonResult AddHoaDon(int Ma,DateTime NgLap,string Mota)
        {
            try
            {
                var hoadon = new HoaDon();
                hoadon.MaKH = Ma;
                hoadon.NgayLap = NgLap;
                hoadon.Mota = Mota;

                db.HoaDons.Add(hoadon);
                db.SaveChanges();

                return Json(new { code = 200, msg = "Thêm thành công !" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm thất bại !"+ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CapNhat(int ID,int Ma, DateTime NgLap, string Mota)
        {
            try
            {
                var hoadon = db.HoaDons.Find(ID);

                hoadon.MaKH = Ma;
                hoadon.NgayLap = NgLap;
                hoadon.Mota = Mota;
                db.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật thành công !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật thất bại !" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult XoaDonHang(int id)
        {
            try
            {
                var donhang = db.HoaDons.Find(id);
                db.HoaDons.Remove(donhang);

                db.SaveChanges();

                return Json(new { code = 200, msg = "xoa don hang thanh cong !" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = "xoa don hang that bai !"+ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
/*
 show len modal them 
xu lu them

xu ly submit

xu ly detail

sau do phan quyen va set trang
 */