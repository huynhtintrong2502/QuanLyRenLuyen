using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class LopDao
    {

        QuanLyRenLuyenDbConText1 db = null;
        public LopDao()
        {
            db = new QuanLyRenLuyenDbConText1();
        }

        public List<Lop> ListAll()
        {
            return db.Lops.ToList();
        }


        public List<Lop> ListCurent()
        {
            DateTime ngaythang =  DateTime.Now;
            int? namHienTai = ngaythang.Year;
            return db.Lops.Where(x=>x.NamBatDau <= namHienTai && x.NamKetThuc >= namHienTai).ToList();
        }


    }
}

