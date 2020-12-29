namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [Key]
        public double MaSV { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        public string PassWords { get; set; }

        public int? LoaiDangNhap { get; set; }

        public int? xoa { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
