namespace Demo_PhanQuyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiTV")]
    public partial class LoaiTV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiTV()
        {
            LoaiTV_Quyen = new HashSet<LoaiTV_Quyen>();
            ThanhViens = new HashSet<ThanhVien>();
        }

        [Key]
        public int MaLoaiTV { get; set; }

        [StringLength(50)]
        public string TenLoai { get; set; }

        [StringLength(10)]
        public string UuDai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoaiTV_Quyen> LoaiTV_Quyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThanhVien> ThanhViens { get; set; }
    }
}
