using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBH.Models;
using System.Collections;
using System.Net.Mail;
using System.Net;

namespace QLBH.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
        QuanLyBanHang db = new QuanLyBanHang();
        public Cart GetCat()
        {
            var cart = Session["Cart"] as Cart; 

            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }

        public ActionResult AddtoCart(int id)
        {
            var pro = db.SanPhams.FirstOrDefault(s => s.MaSP == id);
            if (pro != null)
            {
                GetCat().Add(pro);
            }

            return RedirectToAction("ShowtoCart", "GioHang");

        }

        public ActionResult ShowtoCart()
        { 
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        public ActionResult Update(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;

            //dữ liệu được truyền qua luôn là 1 chuỗi nên passqua
            int id_pro = int.Parse(form["masp"]);
            int soluong = int.Parse(form["soluong"]);

            cart.update_soluong(id_pro, soluong);
            return RedirectToAction("ShowtoCart");
        }

        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;

            cart.xoa(id);
            return RedirectToAction("ShowtoCart");
        }

        //tao 1 cai partailview de reder no vao
        public PartialViewResult BagCart()
        {
            //gio hang ban dau bang  = 0;
            int soluong = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                soluong = cart.TongSoluong();
            }

            ViewBag.tongsoluong = soluong;
            return PartialView();
        }

        public ActionResult ShoppingSessec()
        {
            return View();
        }

        public void Order(string email)
        {
            Cart cart = Session["Cart"] as Cart;
            string msg = "<html><body><table cellpadding='0' cellspacing='0' width='500px' align='left' border='1'><caption>Thông tin đặt hàng</caption><tr><th>STT</th><th>Tên sản phẩm</th><th>Số lượng</th><th>Đơn giá</th><th>Thành tiền</th></tr>";

            int i = 0;
            double tongtien = cart.tongtien();
            foreach (var item in cart.Items)
            {
                i++;
                msg += "<tr>";
                msg += "<td>" + i.ToString() + "</td>";
                msg += "<td>" + item.pro.TenSP + "</td>";
                msg += "<td>" + item.soluong.ToString() + "</td>";
                msg += "<td>" + String.Format("{0:#,###}",item.pro.DonGia) + "</td>";
                msg += "<td>" + String.Format("{0:#,### vnđ}", item.soluong* item.pro.DonGia) + "</td>";
                msg += "</tr>";
            }
            msg += "<tr><th colspan='5'>Tổng cộng:" + String.Format("{0:#,###} vnđ", tongtien) + "</th></tr></table></body></html>";
            MailMessage mail = new MailMessage("ngtquoc163@gmail.com", email, "Thông tin đơn hàng", msg);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("ngtquoc163@gmail.com", "ntq955706");
            mail.IsBodyHtml = true;
            client.Send(mail);

            //gởi email cho khách hàng !

        }

        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                //Luu thong tin khach hang
                KhachHang kh = new KhachHang();
                kh.TenKH = form["txtname"];

             
                kh.DienThoai = form["txtdienthoai"];
                kh.Email = form["txtemail"];

                //Thay thế bằng mã khách hàng !
                kh.Code = int.Parse(form["txtcode"]);

                db.KhachHangs.Add(kh);

                Cart cart = Session["Cart"] as Cart;

                //Lưu vào bảng hóa đơn
                HoaDon hoadon = new HoaDon();
                hoadon.NgayLap = DateTime.Now;
                hoadon.NgayGiaoHang = DateTime.Now;

                //thay thế bằng mã khách hàng
                hoadon.CosCustomer = int.Parse(form["txtcode"]);

                hoadon.DiaChi = form["Address"];//dia chi giao cua don hang
                db.HoaDons.Add(hoadon);

                //dung vong lap de lay cac item trong gio hang de luu vao trog bang order details
                foreach(var item in cart.Items)
                {
                    CTHD ct = new CTHD();
                    ct.MaHD = hoadon.MaHD;
                    ct.MaSP = item.pro.MaSP;
                    
                    ct.SoLuong = item.soluong;
                    ct.DonGia = item.pro.DonGia;
                    ct.GiamGia = 1;
                    db.CTHDs.Add(ct);
                }
                db.SaveChanges();
             

                var kh1= db.KhachHangs.Where(k => k.Code == hoadon.CosCustomer).FirstOrDefault();

                //gửi email đến khách hàng
                var emailkh = kh.Email;
                Order(emailkh);

                cart.Clear_GioHang();

                //hIỂN THỊ THÔNG BÁO ! Mua hàng thành công 

                return RedirectToAction("ShoppingSessec", "GioHang");
            }
            catch
            {
                return Content("Error Checkout.Please !");
            }
        }
    }
}