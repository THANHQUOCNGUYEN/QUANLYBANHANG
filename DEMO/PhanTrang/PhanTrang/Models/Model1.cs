using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PhanTrang.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<CTHD> CTHDs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiSP> LoaiSPs { get; set; }
        public virtual DbSet<LoaiTV> LoaiTVs { get; set; }
        public virtual DbSet<LoaiTV_Quyen> LoaiTV_Quyen { get; set; }
        public virtual DbSet<Product_HinhAnh> Product_HinhAnh { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.CTHDs)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.KhachHang)
                .HasForeignKey(e => e.CosCustomer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiTV>()
                .HasMany(e => e.LoaiTV_Quyen)
                .WithRequired(e => e.LoaiTV)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiTV>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.LoaiTV1)
                .HasForeignKey(e => e.LoaiTV);

            modelBuilder.Entity<LoaiTV_Quyen>()
                .Property(e => e.MaQuyen)
                .IsUnicode(false);

            modelBuilder.Entity<Quyen>()
                .Property(e => e.MaQuyen)
                .IsUnicode(false);

            modelBuilder.Entity<Quyen>()
                .HasMany(e => e.LoaiTV_Quyen)
                .WithRequired(e => e.Quyen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.DonViTinh)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.CTHDs)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.Product_HinhAnh)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);
        }
    }
}
