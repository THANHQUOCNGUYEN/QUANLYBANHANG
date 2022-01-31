using OfficeOpenXml;
using PagedList;
using QLBH.Models;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace QLBH.Controllers
{
    public class SanPhamsController : Controller
    {
        private QuanLyBanHang db = new QuanLyBanHang();

        // GET: SanPhams
        public ActionResult Index(string txttim, int? page,int? gia)
        {
            if (TempData["result"] != null)
            {
                ViewBag.Success = TempData["result"];
            }
            if(page == null)
            {
                page = 1;
            }
           
            //Xu ly giathanh
            //Phan trang danh sach san pham
            var danhsach = db.SanPhams.OrderBy(s => s.MaSP).ToList();
            ViewBag.Pagecurrent = page;
            ViewBag.pagenumber = danhsach.Count % 5 == 0 ? danhsach.Count / 5 : danhsach.Count / 5 + 1;
            var listhientai = danhsach.Skip((int)(page - 1) * 5).Take(5).ToList();
            //xu ly tim kiem
            if (txttim != null)
            {
                danhsach = db.SanPhams.Where(s => s.TenSP.Contains(txttim)).OrderBy(s => s.MaSP).ToList();
                ViewBag.Pagecurrent = page;
                ViewBag.tim = txttim;
                ViewBag.pagenumber = danhsach.Count % 5 == 0 ? danhsach.Count / 5 : danhsach.Count / 5 + 1;
                listhientai = danhsach.Skip((int)(page - 1) * 5).Take(5).ToList();
            }
            //xu ly gia thanh
            if (gia != null)
            {
                if(gia == 1)
                {
                    danhsach = db.SanPhams.Where(s=>s.DonGia<10000000).OrderBy(s => s.MaSP).ToList();
                    ViewBag.Pagecurrent = page;
                    ViewBag.pagenumber = danhsach.Count % 5 == 0 ? danhsach.Count / 5 : danhsach.Count / 5 + 1;
                    ViewBag.gia = gia;
                    listhientai = danhsach.Skip((int)(page - 1) * 5).Take(5).ToList();
                }
                else
                {
                    danhsach = db.SanPhams.Where(s => s.DonGia >= 10000000).OrderBy(s => s.MaSP).ToList();
                    ViewBag.Pagecurrent = page;
                    ViewBag.gia = gia;
                    ViewBag.pagenumber = danhsach.Count % 5 == 0 ? danhsach.Count / 5 : danhsach.Count / 5 + 1;
                    listhientai = danhsach.Skip((int)(page - 1) * 5).Take(5).ToList();
                }
            }
           
            return View(listhientai);
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            int maloai = sanPham.LoaiSP.MaLoaiSP;

            var list = db.SanPhams.Where(s => s.MaLoaiSP == maloai);
            var lstthunho = db.Product_HinhAnh.Where(s => s.MaSP == sanPham.MaSP);
            ViewBag.lstthunho = lstthunho;
            ViewBag.list = list;

            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,DonViTinh,DonGia,MaLoaiSP,HinhSP")] SanPham sanPham, HttpPostedFileBase HINH)
        {
            if (ModelState.IsValid)
            {
                if (HINH != null && HINH.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(HINH.FileName);
                    string path = Server.MapPath("~/Content/img/" + fileName);
                    sanPham.HinhSP = "~/Content/img/" + fileName;

                    HINH.SaveAs(path);

                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();

                    TempData["Result"] = "Tạo mới thành công !";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Vui lòng chọn hình sản phẩm !";
                }

            }

            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5

        //Kiểm tra xem file upload có phải là phải hình ảnh hay không?
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        //Hàm kiểm tra phần mở rộng
        public bool CheckFileType(string fileName)
        {

            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".gif":
                    return true;
                case ".png":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,DonViTinh,DonGia,MaLoaiSP,HinhSP")] SanPham sanPham, HttpPostedFileBase Hinhupload, string HinhSP)
        {
            if (ModelState.IsValid)
            {
                //kiểm tra phần mở rộng?
                if (Hinhupload != null && Hinhupload.ContentLength > 0)
                {

                    if (CheckFileType(Hinhupload.FileName))
                    {
                        string fileName = Path.GetFileName(Hinhupload.FileName);
                        string path = Server.MapPath("~/Content/img/" + fileName);

                        Hinhupload.SaveAs(path);
                        sanPham.HinhSP = "~/Content/img/" + fileName;

                        //Phần hồi nãy bỏ vô đây !
                        db.Entry(sanPham).State = EntityState.Modified;
                        db.SaveChanges();

                        TempData["result"] = "Chỉnh sửa thành công !";
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        //sanPham.HinhSP = HinhSP;
                        ViewBag.error = "file chưa đúng định dạng !";
                    }

                }
                else
                {
                    //sanPham.HinhSP = HinhSP;
                    db.Entry(sanPham).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["result"] = "Chỉnh sửa thành công !";
                    return RedirectToAction("Index");
                }

            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
            
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();

            System.IO.File.Delete(Server.MapPath(sanPham.HinhSP));

            TempData["result"] = "Xóa thành công !";
            return RedirectToAction("Index");
        }



        //Lấy tên danh sách sản phẩm có chứa từ khóa tìm kiếm !

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //auto complete

        public ActionResult Xuat()
        {
            var list = db.SanPhams.ToList();

            //cach ghi khac

            //neu lay ten san pham
            var sps = db.SanPhams.Select(s => s.TenSP);

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");


            //tao tieu de
            //dien thong thong cac cot tuong ung vao work sheet
            ws.Cells["A1"].Value = "Ma SP";
            ws.Cells["B1"].Value = "Ma Ten SP";
            ws.Cells["c1"].Value = "Ma SP";
            ws.Cells["D1"].Value = "Ma SP";
            ws.Cells["E1"].Value = "Ma SP";
            ws.Cells["F1"].Value = "Ma SP";
            //bat dau tu dong thu 2 vi dong thu nhat la tieu de
            int starRow = 2;
            foreach (var item in list)
            {

                //to mau cho dong
                if (item.DonGia < 10)
                {

                    ws.Row(starRow).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(starRow).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("red")));
                }
                ws.Cells[string.Format("A{0}", starRow)].Value = item.MaSP;
                ws.Cells[string.Format("B{0}", starRow)].Value = item.TenSP;
                ws.Cells[string.Format("C{0}", starRow)].Value = item.DonViTinh;
                ws.Cells[string.Format("D{0}", starRow)].Value = item.DonGia;
                ws.Cells[string.Format("E{0}", starRow)].Value = item.MaLoaiSP;
                ws.Cells[string.Format("F{0}", starRow)].Value = item.HinhSP;
                starRow++;

            }
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-disposition", "attachment: filename" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();


            return View();
        }

        public JsonResult getname(string txttim)
        {

            var list = db.SanPhams.Where(s => s.TenSP.Contains(txttim)).Select(s => s.TenSP).ToList();
            return Json(list);
        }

        
    }
}
