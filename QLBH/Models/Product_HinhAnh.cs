namespace QLBH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_HinhAnh
    {
        public int ID { get; set; }

        public int MaSP { get; set; }

        public string HinhAnh { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
