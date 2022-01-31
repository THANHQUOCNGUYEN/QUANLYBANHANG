using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using QLBH.Common;
using QLBH.Models;
using Rotativa;
namespace QLBH.Controllers
{
    public class HoaDonController : Controller
    {
        public QuanLyBanHang db = new QuanLyBanHang();
        // GET: HoaDon
        public ActionResult Index()
        {
            if (TempData["Delete"] != null)
            {
                ViewBag.Success = TempData["Delete"];
            }
            var lst = db.HoaDons.ToList();
            return View(lst);
        }

        public ActionResult ChiTiet(int? id)
        {
            TempData["id"] = id;
            var lst = db.CTHDs.Where(s => s.MaHD == id).ToList();
            return View(lst);
        }

        public ActionResult Delete(int id)
        {
            var hoadon = db.HoaDons.Find(id);

            db.HoaDons.Remove(hoadon);
            db.SaveChanges();

            TempData["Delete"] = "Xóa Thành Công !";

            return RedirectToAction("Index");
        }


        public ActionResult Create(HoaDon hd)
        {
            return View();
        }
        //public FileStreamResult ExporttoPdf()
        //{
        //    int id = (int)TempData["id"];
        //    var all = (from hd in db.CTHDs.Where(s => s.MaHD == id)
        //               join kh in db.KhachHangs on hd.HoaDon.KhachHang.MaKH equals kh.MaKH
        //               select new
        //               {
        //                   tenkh = kh.TenKH,
        //                   tensp = hd.SanPham.TenSP,
        //                   dongia = hd.DonGia,
        //                   soluong = hd.SoLuong,
        //                   giamgia = hd.GiamGia,
        //                   thanhtien = hd.SoLuong*hd.DonGia
        //               }).ToList();
        //    WebGrid grid = new WebGrid(source: all, canPage: false, canSort: false);
        //    string anhquoc = all.FirstOrDefault().tenkh;
        //    double tongtien = 0;
        //    foreach(var item in all)
        //    {
        //        tongtien += (double)item.thanhtien;
        //    }
        //    DateTime dt = DateTime.Now;
        //    string gridHtml = grid.GetHtml(

        //        columns: grid.Columns(

        //            grid.Column("tensp", "San Pham"),
        //            grid.Column("dongia", "Don Gia"),
        //            grid.Column("soluong", "So Luong"),
        //            grid.Column("giamgia", "Giam Gia"),
        //            grid.Column("thanhtien", "Thanh Tien")
        //            )
        //        )
        //    .ToString();
        //    //Tach rieng ham string de de dang dinh dang css cho no
        //    string html = "<html>" +
        //                        "<head>{0}</head>" +
        //                        "<body><b>HOA DON BAN HANG</b>{1}<b style='text-align:center'>THANH TIEN : {2}</b></body>" +
        //                  "</html>";
        //    string export = string.Format(html,"<style>table{border-spacing:20px;border-collapse:separate;}</style>", gridHtml,tongtien);
        //    var bytes = System.Text.Encoding.UTF8.GetBytes(export);
        //    using (var input = new MemoryStream(bytes)) 
        //    {   
        //        var output = new MemoryStream();
        //        var document = new iTextSharp.text.Document(PageSize.A4.Rotate(), 200, 50, 200, 50);
        //        var writer = PdfWriter.GetInstance(document, output);
        //        writer.CloseStream = false;
        //        document.Open();

        //        var xmlWorker = iTextSharp.tool.xml.XMLWorkerHelper.GetInstance();
        //        xmlWorker.ParseXHtml(writer, document, input, System.Text.Encoding.UTF8);

        //        document.Close();
        //        output.Position = 0;
        //        return new FileStreamResult(output, "application/pdf");
        //    } `
        //}

        //In hoa don dang Partiview

        public ActionResult PrintAll()
        {
            int mahoadon = (int)TempData["id"];
            var q = new ActionAsPdf("ChiTiet", new { id = mahoadon });
            return q;
        }

        [HttpPost]
        public ActionResult Create(int id, HoaDon hd)
        {
            return View();
        }

        public async Task<ActionResult> ExportPdf(int id)
        {
            string fileName = string.Concat("Product" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".pdf");
            var folderReport = "/FolderPdf";
            string filePath = Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fullPath = Path.Combine(filePath, fileName);
            var template = System.IO.File.ReadAllText(Server.MapPath("/Template/hoadons.html"));
            var replaces = new Dictionary<string, string>();
            var listhoadon = db.CTHDs.Where(s => s.MaHD == id).ToList();
            var html = "";
            double tongtien = 0;
            int i = 0;
            foreach (var item in listhoadon)
            {
                i++;
                tongtien += Convert.ToDouble(item.SoLuong * item.SanPham.DonGia);
                html += "<tr><td>" + i + "</td><td>" + item.SanPham.TenSP + "</td><td>" + item.SoLuong + "</td><td>" + item.SanPham.DonGia + "</td><td>" + item.SoLuong* item.SanPham.DonGia + "</td></tr><hr style='border-top: dotted 1px;' />";
            }
            replaces.Add("{{Content}}", html);
            replaces.Add("{{tongtien}}", string.Format("{0:#,##}",tongtien));
            template = template.Parse(replaces);

            await ReportHelper.GeneratePdf(template, fullPath);

            return Redirect(Path.Combine(folderReport, fileName));// thằng này kiểu string
        }
    }
}