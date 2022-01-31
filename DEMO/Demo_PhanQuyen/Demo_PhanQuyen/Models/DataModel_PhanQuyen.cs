using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Demo_PhanQuyen.Models
{
    public partial class DataModel_PhanQuyen : DbContext
    {
        public DataModel_PhanQuyen()
            : base("name=DataModel_PhanQuyen")
        {
        }

        public virtual DbSet<LoaiTV> LoaiTVs { get; set; }
        public virtual DbSet<LoaiTV_Quyen> LoaiTV_Quyen { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<ThanhVien> ThanhViens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoaiTV>()
                .Property(e => e.UuDai)
                .IsFixedLength();

            modelBuilder.Entity<LoaiTV>()
                .HasMany(e => e.LoaiTV_Quyen)
                .WithRequired(e => e.LoaiTV)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiTV_Quyen>()
                .Property(e => e.maquyen)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiTV_Quyen>()
                .Property(e => e.ghichu)
                .IsFixedLength();

            modelBuilder.Entity<Quyen>()
                .Property(e => e.MaQuyen)
                .IsUnicode(false);

            modelBuilder.Entity<Quyen>()
                .HasMany(e => e.LoaiTV_Quyen)
                .WithRequired(e => e.Quyen)
                .WillCascadeOnDelete(false);
        }
    }
}
