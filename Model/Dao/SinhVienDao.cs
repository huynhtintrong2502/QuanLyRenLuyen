using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace Model.Dao
{
    public class SinhVienDao
    {
        QuanLyRenLuyenDbConText1 db = null;
        public SinhVienDao()
        {
            db = new QuanLyRenLuyenDbConText1();
        }
        public List<SinhVien> ListSinhVien()
        {
            return db.SinhViens.OrderByDescending(x => x.MaSV).Where(x=>x.xoa ==0).ToList();
        }

        public SinhVien GetSV(double MaSV)
        {
            return db.SinhViens.Where(x => x.xoa == 0 && x.MaSV == MaSV).SingleOrDefault();
        }

        public void XoaSV(User user)
        {
            var ur = db.Users.Find(user.MaSV);
            var SV = db.SinhViens.Find(user.MaSV);
            var DRL = db.DiemRenLuyens.Where(x=>x.MaSV == user.MaSV).ToList();
            if (ur !=null && SV != null && DRL != null)
            {
                ur.xoa = 1;
                SV.xoa = 1;
                foreach(var item in DRL)
                    item.Xoa = 1;
                db.SaveChanges();
            }
        }

        public void Insert(SinhVien SV)
        {
            db.SinhViens.Add(SV);
            db.SaveChanges();
        }

        public void create(double MaSV,string HoTen)
        {
            var SV = new SinhVien();
            SV.MaSV = MaSV;
            SV.HoTen = HoTen;
            SV.Anh = "/assets/client/image/anhmacdinh.jpg";
            SV.xoa = 0;
            Insert(SV);
        }

        public void Update(SinhVien SV, DateTime? ngaySinh)
        {
            var sinhVien = GetSV(SV.MaSV);
            
            sinhVien.HoTen = SV.HoTen;
            sinhVien.GioiTinh = SV.GioiTinh;
            sinhVien.SoDT = SV.SoDT;
            sinhVien.Email = SV.Email;
            sinhVien.NgaySinh = ngaySinh;
            sinhVien.NoiSinh = SV.NoiSinh;
            sinhVien.ChoTamTru = SV.ChoTamTru;
            sinhVien.ChucVu = SV.ChucVu;
            sinhVien.xoa = 0;
            sinhVien.Id_Lop = SV.Id_Lop;
            db.SaveChanges();
        }

        public List<SinhVien> ListSinhVienSearch(string search_text)
        {
            if (search_text == null || search_text.Equals(""))
                return ListSinhVien();
            else
            {
                List<SinhVien> list = db.SinhViens.OrderByDescending(x => x.MaSV).Where(x => x.HoTen == search_text && x.xoa == 0).ToList();
                if (list.Count() == 0)
                {
                    if (IsNumber(search_text))
                    {
                        double a = Double.Parse(search_text);
                        list = db.SinhViens.OrderByDescending(x => x.MaSV).Where(x => x.MaSV == a && x.xoa == 0).ToList();
                        if (list.Count() == 0)
                        {
                            return ListSinhVien();
                        }
                        else return list;
                    }
                    else return ListSinhVien();
                }
                else return list;
            }
            
        }

        private bool IsNumber(string s)
        {
            foreach (Char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }
}
