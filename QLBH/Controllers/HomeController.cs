using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBH.Models;
using PagedList;
using System.Web.Security;
using System.Collections;

namespace QLBH.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanHang db = new QuanLyBanHang();
        public ActionResult Index(int? maloaisp, int? pageIndex, string txttim,string giathanh)
        {
           
           if(TempData["ThanhCong"] != null)
            {
                ViewBag.ThanhCong = TempData["ThanhCong"];
            }

            if(giathanh == "true")
            {
                //
            }
            //Kiểm tra txtim có null hay không?
            if(txttim != null)
            {
                if (pageIndex == null) pageIndex = 1;

                var links = db.SanPhams.Where(s=>s.TenSP.Contains(txttim)).OrderBy(s => s.MaSP);

                int pageSize = 12;
                int pageNumber = (pageIndex ?? 1);
                ViewBag.tim = txttim;
                return View(links.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                //Nếu txttim == null tiếp tục kiểm tra đến mã loại sản phẩm
                if (maloaisp == null)
                {
                    if (pageIndex == null) pageIndex = 1;

                    var links = db.SanPhams.OrderBy(s => s.MaSP);
                    int pageSize = 12;
                    int pageNumber = (pageIndex ?? 1);
                    
                    return View(links.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    ViewBag.ma = maloaisp;
                    if (pageIndex == null) pageIndex = 1;

                    var links = db.SanPhams.Where(s => s.MaLoaiSP == maloaisp).OrderBy(s => s.MaSP);
                    int pageSize = 12;
                    int pageNumber = (pageIndex ?? 1);

                    ViewBag.loaisp = maloaisp;
                    return View(links.ToPagedList(pageNumber, pageSize));
                }
            }
        }
        public ActionResult timkiem(string txttim, int? page)
        {
            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var links = db.SanPhams.OrderBy(s => s.MaSP);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 3;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LayTenSP()
        {
            var list = db.LoaiSPs.ToList();
            //Hashtable tenloaisp = new Hashtable();
            //foreach (var item in list)
            //{
            //    tenloaisp.Add(item.MaLoaiSP, item.TenLoaiSP);
            //}
            ViewBag.TenLoai = list;
            return PartialView();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //LAY Thong tin khach hang
            var khachhang = db.KhachHangs.Find(123);


            return View(khachhang);
        }
    }
}