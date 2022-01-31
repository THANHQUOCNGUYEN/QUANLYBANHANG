namespace Demo_PhanQuyen.Models
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
        public string maquyen { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maloaiTV { get; set; }

        [StringLength(10)]
        public string ghichu { get; set; }

        public virtual LoaiTV LoaiTV { get; set; }

        public virtual Quyen Quyen { get; set; }
    }
}
