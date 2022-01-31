using QLBH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBH.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        QuanLyBanHang db = new QuanLyBanHang();
        public ActionResult Index()
        {
            ViewBag.PageView = HttpContext.Application["PageView"].ToString();//Lấy số lượng người truy câp
            ViewBag.SonguoiDangOnline = HttpContext.Application["SonguoiDangOnline"].ToString();
            ViewBag.TongDoanhThu = ThongKeDoangThu();//Thống kê tổng doanh thu
            ViewBag.SoluongThanhVien = ThongKeThanhVien();


            return View();
        }

        //Đếm số lượng thành viên
        public int ThongKeThanhVien()
        {
            
            int slthanhvien = db.Users.Count();
            return slthanhvien;
        }

        //Tong doanh thu
        public double ThongKeDoangThu()
        {
            //Thong ke theo tat ca doanh thu tu khi web site thanh lap
            double TongDoanhThu = (double)db.CTHDs.Sum(s => s.SoLuong * s.DonGia);

            return TongDoanhThu;
        }


        //Tong doanh thu theo thang ,nam
        public double ThongKeDoanhThu(int thang, int nam)
        {
            //Thong ke theo tat ca doanh thu tu khi web site thanh lap
            //list ra nhung don hang nao co thang nam tuong ung


            var list = db.HoaDons.Where(s => s.NgayLap.Value.Month == thang && s.NgayLap.Value.Year == nam);
            double tongtien = 0;
            foreach (var item in list)
            {
                tongtien += item.CTHDs.Sum(n => n.SoLuong * n.GiamGia);
            }


            return tongtien;
        }

        [HttpPost]
        public ActionResult thongkesoluongsanphambanduoc()
        {
            //group by theo mã sản phẩm
            var query = db.CTHDs
                .GroupBy(s => s.SanPham.TenSP)
                .Select(g => new { name = g.Key, count = g.Sum(s => s.SoLuong * s.DonGia) }).ToList();
            return Json(query);
        }

        //Thêm hàm này
        public ActionResult doanhthumoinam()
        {
            var query = db.CTHDs
                .GroupBy(s => s.HoaDon.NgayLap.Value.Year)
                .Select(g => new { name = g.Key, count = g.Sum(s => s.SoLuong * s.DonGia) }).ToList();

            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult demoResposive()
        {
            return View();
        }
     
        public ActionResult chart()
        {
            return View();
        }
       [HttpPost]
        public JsonResult staticRevenue(DateTime fromdate,DateTime todate)
        {
            var query = db.CTHDs.Where(s=>s.HoaDon.NgayLap>=fromdate && s.HoaDon.NgayLap<=todate).GroupBy(s => s.SanPham.TenSP)
                .Select(g => new { name = g.Key,count = g.Sum(s=>s.SoLuong*s.DonGia) });

            return Json(query);
        }
        
    }
}
