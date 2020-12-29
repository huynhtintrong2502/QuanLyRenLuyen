namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DiemRenLuyen_Lop
    {
        [Key]
        [Column(Order = 0)]
        public double MaSV { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_NamHoc { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_HocKi { get; set; }

        public int? Muc1_1 { get; set; }

        public int? Muc1_2 { get; set; }

        public int? Muc1_3 { get; set; }

        public int? Muc1_4 { get; set; }

        public int? Muc2_1 { get; set; }

        public int? Muc2_2 { get; set; }

        public int? Muc2_3 { get; set; }

        public int? Muc3_1 { get; set; }

        public int? Muc3_2 { get; set; }

        public int? Muc4_1 { get; set; }

        public int? Muc4_2 { get; set; }

        public int? Muc4_3 { get; set; }

        public int? Muc5_1 { get; set; }

        public int? Muc5_2 { get; set; }

        public int? Muc5_3 { get; set; }

        public int? Muc6_1 { get; set; }

        public int? DD_Muc1_1 { get; set; }

        public int? DD_Muc1_2 { get; set; }

        public int? DD_Muc1_3 { get; set; }

        public int? DD_Muc1_4 { get; set; }

        public int? DD_Muc2_1 { get; set; }

        public int? DD_Muc2_2 { get; set; }

        public int? DD_Muc2_3 { get; set; }

        public int? DD_Muc3_1 { get; set; }

        public int? DD_Muc3_2 { get; set; }

        public int? DD_Muc4_1 { get; set; }

        public int? DD_Muc4_2 { get; set; }

        public int? DD_Muc4_3 { get; set; }

        public int? DD_Muc5_1 { get; set; }

        public int? DD_Muc5_2 { get; set; }

        public int? DD_Muc5_3 { get; set; }

        public int? DD_Muc6_1 { get; set; }

        public double? DiemKyNay { get; set; }

        public double? DiemKyTruoc { get; set; }

        public int? Dem { get; set; }

        public int? Xoa { get; set; }

        public int? TongMuc1 { get; set; }

        public int? TongMuc2 { get; set; }

        public int? TongMuc3 { get; set; }

        public int? TongMuc4 { get; set; }

        public int? TongMuc5 { get; set; }

        public DateTime? DateUpdate { get; set; }

        public virtual NamHoc NamHoc { get; set; }

        public virtual HocKy HocKy { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
