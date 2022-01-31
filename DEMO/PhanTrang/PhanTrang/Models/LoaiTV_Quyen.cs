namespace PhanTrang.Models
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
        [StringLength(20)]
        public string MaQuyen { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaLoaiTV { get; set; }

        [StringLength(50)]
        public string MoTa { get; set; }

        public virtual LoaiTV LoaiTV { get; set; }

        public virtual Quyen Quyen { get; set; }
    }
}
