using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH.Models
{
    public class Cart
    {
        //bao gồm 1 list các sản phẩm
        List<CartItems> items = new List<CartItems>();

        public IEnumerable<CartItems> Items
        {
            get
            {
                return items;
            }
        }

        public void Add(SanPham sp,int sl = 1)
        {
            var pro1 = items.FirstOrDefault(s => s.pro.MaSP == sp.MaSP);
            //dùng find 
            

            if(pro1 == null)
            {
                CartItems i = new CartItems();
                i.pro = sp;
                i.soluong = sl;
                items.Add(i);
            }
            else
            {
                pro1.soluong += sl;
            }
        }

        public void update_soluong(int i ,int soluong)
        {
            var item = items.Find(s => s.pro.MaSP == i);
            if(item != null)
            {
                item.soluong = soluong;
            }
        }

        public double tongtien()
        {
            var tt = items.Sum(s =>s.pro.DonGia*s.soluong);
            return (double)tt;
        }

        public void xoa(int id)
        {
            var item = items.Find(s => s.pro.MaSP == id);
            items.Remove(item);
        }

        public int TongSoluong()
        {
            int sl = items.Sum(s => s.soluong);
            return sl;
        }

        public void Clear_GioHang()
        {
            items.Clear();
        }
      }
}