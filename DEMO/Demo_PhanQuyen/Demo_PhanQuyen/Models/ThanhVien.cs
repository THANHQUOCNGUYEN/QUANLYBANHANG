namespace Demo_PhanQuyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThanhVien")]
    public partial class ThanhVien
    {
        [Key]
        public int maTV { get; set; }

        [StringLength(50)]
        public string tenTV { get; set; }

        public int? maloaiTV { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [StringLength(50)]
        public string password { get; set; }

        public virtual LoaiTV LoaiTV { get; set; }
    }
}
