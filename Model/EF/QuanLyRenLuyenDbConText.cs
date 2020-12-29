namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QuanLyRenLuyenDbConText : DbContext
    {
        public QuanLyRenLuyenDbConText()
            : base("name=QuanLyRenLuyenDbConText")
        {
        }

        public virtual DbSet<DiemRenLuyen> DiemRenLuyens { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiemRenLuyen>()
                .Property(e => e.NamHoc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.SoDT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.Email)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.Lop)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.SinhVien)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete();
        }
    }
}
