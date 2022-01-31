using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Ajax_Demo.Models;
namespace Web_Ajax_Demo.Controllers
{
    public class KhachHangController : Controller
    {
        public ModeHocSinh db = new ModeHocSinh();
        // GET: KhachHang
        public ActionResult Index()
        {
            return View();
        }

        //Lay danh sach khach hang combobox

        public JsonResult danhsach()
        {
            var lst = (from kh in db.KhachHangs
                       select new
                       {
                           makh = kh.MaKH,
                           tenkh = kh.TenKH
                       }).ToList();

            return Json(new {code=200, lst = lst }, JsonRequestBehavior.AllowGet);
        }

        //Load KhachHang
        [HttpGet]
        public JsonResult dsKhachHang(int trang,string tukhoa)
        {
            try
            {
                int pagesize = 3;
                var lstkh = (from kh in db.KhachHangs
                             select new
                             {
                                 makh = kh.MaKH,
                                 tenkh = kh.TenKH,
                                 gioitinh = kh.GioiTinh,
                                 ngsinh = kh.NgaySinh.Value.ToString(),
                                 diachi = kh.DiaChi,
                                 dienthoai = kh.DienThoai
                             }).Where(s => s.tenkh.Contains(tukhoa)).ToList();
                
                var kqht = lstkh.Skip((trang - 1) * pagesize).Take(pagesize);

                var sotrang = lstkh.Count % pagesize == 0 ? lstkh.Count / pagesize : lstkh.Count / pagesize + 1;

                return Json(new { code = 200, dskh = kqht,soTrang = sotrang }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { code = 500, msg = ex.Message },JsonRequestBehavior.AllowGet);
            }
           
        }

        [HttpPost]
        public JsonResult ThemKhachHang(string tenkh,bool gioitinh,DateTime ngsinh,string diachi,string dienthoai)
        {
            try
            {
                var khachhang = new KhachHang();
                khachhang.TenKH = tenkh;
                khachhang.GioiTinh = gioitinh;
                khachhang.NgaySinh = ngsinh;
                khachhang.DiaChi = diachi;
                khachhang.DienThoai = dienthoai;

                db.KhachHangs.Add(khachhang);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm thành công !" }, JsonRequestBehavior.AllowGet);
            }catch(Exception ex)
            {
                return Json(new { code = 500, msg = "Error " + ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CapNhatKhachHang(int makh, string tenkh, bool gioitinh, DateTime ngsinh, string diachi, string dienthoai)
        {
            try
            {
                var khachhang = db.KhachHangs.Find(makh);
                khachhang.TenKH = tenkh;
                khachhang.GioiTinh = gioitinh;
                khachhang.NgaySinh = ngsinh;
                khachhang.DiaChi = diachi;
                khachhang.DienThoai = dienthoai;

                db.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật thành công !" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { code = 500, msg = "Cập nhật thất bại !" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult XoaKhachHang(int makh)
        {
            try
            {
                //truong hop xoa
                var khachhang = db.KhachHangs.Find(makh);
                db.KhachHangs.Remove(khachhang);

                //truong hop cap nhat trang thai
                db.SaveChanges();

                return Json(new { code = 200, msg = "Xoa thanh cong !" }, JsonRequestBehavior.AllowGet);
            }
           
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xoa that bai !"+ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

//Theo chuan seo