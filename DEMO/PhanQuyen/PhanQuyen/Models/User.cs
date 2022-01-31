namespace PhanQuyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [Key]
        [StringLength(50)]
        public string tendangnhap { get; set; }

        [StringLength(50)]
        public string matkhau { get; set; }

        [StringLength(50)]
        public string nhaclaipassword { get; set; }

        
        public bool? gioitinh { get; set; }

        public DateTime? ngsinh { get; set; }

        [StringLength(50)]
        public string maloaitv { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
