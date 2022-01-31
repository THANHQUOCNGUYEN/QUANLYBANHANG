namespace Web_Ajax_Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        public int MaHD { get; set; }

        public int? MaKH { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayLap { get; set; }


        [StringLength(50)]
        public string Mota { get; set; }

        public virtual KhachHang KhachHang { get; set; }


    }
}
