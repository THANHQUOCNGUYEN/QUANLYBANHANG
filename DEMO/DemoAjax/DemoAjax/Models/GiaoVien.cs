namespace DemoAjax.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GiaoVien")]
    public partial class GiaoVien
    {
        [Key]
        public int MaGV { get; set; }

        [StringLength(50)]
        public string TenGV { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgSinh { get; set; }

        public bool? GioiTinh { get; set; }

        public int? MaMonHoc { get; set; }

        public virtual MonHoc MonHoc { get; set; }
    }
}
