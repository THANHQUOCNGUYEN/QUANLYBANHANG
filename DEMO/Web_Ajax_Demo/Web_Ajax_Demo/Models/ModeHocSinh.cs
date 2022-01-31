using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Web_Ajax_Demo.Models
{
    public partial class ModeHocSinh : DbContext
    {
        public ModeHocSinh()
            : base("name=ModeHocSinh")
        {
        }

        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LopHoc> LopHocs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KhachHang>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<LopHoc>()
                .Property(e => e.tenlop)
                .IsFixedLength();

            modelBuilder.Entity<LopHoc>()
                .Property(e => e.mota)
                .IsFixedLength();
        }
    }
}
