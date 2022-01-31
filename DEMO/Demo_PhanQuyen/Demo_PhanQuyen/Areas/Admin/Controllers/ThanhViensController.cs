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
    [Authorize(Roles = "QUANTRI,QLSP")]
    public class ThanhViensController : Controller
    {
        private DataModel_PhanQuyen db = new DataModel_PhanQuyen();

        // GET: Admin/ThanhViens
        public ActionResult Index()
        {
            var thanhViens = db.ThanhViens.Include(t => t.LoaiTV);
            return View(thanhViens.ToList());
        }

        // GET: Admin/ThanhViens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhVien thanhVien = db.ThanhViens.Find(id);
            if (thanhVien == null)
            {
                return HttpNotFound();
            }
            return View(thanhVien);
        }

        // GET: Admin/ThanhViens/Create
        public ActionResult Create()
        {
            ViewBag.maloaiTV = new SelectList(db.LoaiTVs, "MaLoaiTV", "TenLoai");
            return View();
        }

        // POST: Admin/ThanhViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maTV,tenTV,maloaiTV,username,password")] ThanhVien thanhVien)
        {
            if (ModelState.IsValid)
            {
                db.ThanhViens.Add(thanhVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maloaiTV = new SelectList(db.LoaiTVs, "MaLoaiTV", "TenLoai", thanhVien.maloaiTV);
            return View(thanhVien);
        }

        // GET: Admin/ThanhViens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhVien thanhVien = db.ThanhViens.Find(id);
            if (thanhVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.maloaiTV = new SelectList(db.LoaiTVs, "MaLoaiTV", "TenLoai", thanhVien.maloaiTV);
            return View(thanhVien);
        }

        // POST: Admin/ThanhViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maTV,tenTV,maloaiTV,username,password")] ThanhVien thanhVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thanhVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maloaiTV = new SelectList(db.LoaiTVs, "MaLoaiTV", "TenLoai", thanhVien.maloaiTV);
            return View(thanhVien);
        }

        // GET: Admin/ThanhViens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhVien thanhVien = db.ThanhViens.Find(id);
            if (thanhVien == null)
            {
                return HttpNotFound();
            }
            return View(thanhVien);
        }

        // POST: Admin/ThanhViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThanhVien thanhVien = db.ThanhViens.Find(id);
            db.ThanhViens.Remove(thanhVien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult Laydanhsach()
        {
            var list = (from tv in db.ThanhViens
                        select new
                        {
                            matv = tv.maTV,
                            tentv = tv.tenTV,

                        }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
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
