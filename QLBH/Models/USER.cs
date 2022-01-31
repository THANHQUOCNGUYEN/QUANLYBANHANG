namespace QLBH.Models
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
        public int IDUser { get; set; }

        [StringLength(50,MinimumLength = 5,ErrorMessage ="Tên đăng nhập phải ít nhất 5 kí tự")]
        [Required(ErrorMessage ="Vui lòng nhập UserName")]
        public string UserName { get; set; }

        [StringLength(50)]

        //[Required(ErrorMessage = "Vui lòng nhập Password")]

        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "ít nhất 1 chữ thường,hoa,kí tự số ")]
        public string Password { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [RegularExpression(@"[A-Z0-9a-z.%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Vui lòng nhập đúng định dạng Email")]
        public string Email { get; set; }

       
        public int? LoaiTV { get; set; }

        public virtual LoaiTV LoaiTV1 { get; set; }
    }
}
