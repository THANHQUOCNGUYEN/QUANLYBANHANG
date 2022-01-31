namespace PhanQuyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LoaiTV_Quyen
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string maloaitv { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string maquyen { get; set; }

        [StringLength(50)]
        public string ghichi { get; set; }

        public virtual Quyen Quyen { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
