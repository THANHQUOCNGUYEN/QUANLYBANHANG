using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PhanQuyen.Models
{
    public partial class ModelPhanQuyen : DbContext
    {
        public ModelPhanQuyen()
            : base("name=ModelPhanQuyen")
        {
        }

        public virtual DbSet<LoaiTV_Quyen> LoaiTV_Quyen { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoaiTV_Quyen>()
                .Property(e => e.maloaitv)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiTV_Quyen>()
                .Property(e => e.maquyen)
                .IsUnicode(false);

            modelBuilder.Entity<Quyen>()
                .Property(e => e.maquyen)
                .IsUnicode(false);

            modelBuilder.Entity<Quyen>()
                .HasMany(e => e.LoaiTV_Quyen)
                .WithRequired(e => e.Quyen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.tendangnhap)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.matkhau)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.nhaclaipassword)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.maloaitv)
                .IsUnicode(false);

            modelBuilder.Entity<UserGroup>()
                .Property(e => e.maloai)
                .IsUnicode(false);

            modelBuilder.Entity<UserGroup>()
                .HasMany(e => e.LoaiTV_Quyen)
                .WithRequired(e => e.UserGroup)
                .HasForeignKey(e => e.maloaitv)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserGroup>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.UserGroup)
                .HasForeignKey(e => e.maloaitv);
        }
    }
}
