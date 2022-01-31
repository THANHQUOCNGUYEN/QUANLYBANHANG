using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DemoAjax.Models
{
    public partial class QuanLyGiaoVien : DbContext
    {
        public QuanLyGiaoVien()
            : base("name=QuanLyGiaoVien")
        {
        }

        public virtual DbSet<GiaoVien> GiaoViens { get; set; }
        public virtual DbSet<MonHoc> MonHocs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
