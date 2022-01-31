namespace Web_Ajax_Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LopHoc")]
    public partial class LopHoc
    {
        [Key]
        public int malop { get; set; }

        [StringLength(10)]
        public string tenlop { get; set; }

        [StringLength(10)]
        public string mota { get; set; }
    }
}
