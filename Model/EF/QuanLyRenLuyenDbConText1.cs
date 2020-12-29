namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QuanLyRenLuyenDbConText1 : DbContext
    {
        public QuanLyRenLuyenDbConText1()
            : base("name=QuanLyRenLuyenDbConText1")
        {
        }

        public virtual DbSet<DiemRenLuyen> DiemRenLuyens { get; set; }
        public virtual DbSet<DiemRenLuyen_Lop> DiemRenLuyen_Lop { get; set; }
        public virtual DbSet<HocKy> HocKies { get; set; }
        public virtual DbSet<Lop> Lops { get; set; }
        public virtual DbSet<NamHoc> NamHocs { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HocKy>()
                .HasMany(e => e.DiemRenLuyens)
                .WithRequired(e => e.HocKy)
                .HasForeignKey(e => e.Id_HocKi);

            modelBuilder.Entity<HocKy>()
                .HasMany(e => e.DiemRenLuyen_Lop)
                .WithRequired(e => e.HocKy)
                .HasForeignKey(e => e.Id_HocKi);

            modelBuilder.Entity<Lop>()
                .HasMany(e => e.SinhViens)
                .WithOptional(e => e.Lop)
                .WillCascadeOnDelete();

            modelBuilder.Entity<NamHoc>()
                .HasMany(e => e.DiemRenLuyens)
                .WithRequired(e => e.NamHoc)
                .HasForeignKey(e => e.Id_NamHoc);

            modelBuilder.Entity<NamHoc>()
                .HasMany(e => e.DiemRenLuyen_Lop)
                .WithRequired(e => e.NamHoc)
                .HasForeignKey(e => e.Id_NamHoc);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.SoDT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.Email)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.SinhVien)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete();
        }
    }
}
