using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class NamHocDao
    {
        QuanLyRenLuyenDbConText1 db = null;
        public NamHocDao()
        {
            db = new QuanLyRenLuyenDbConText1();
        }

        public List<NamHoc> ListAll()
        {
            return db.NamHocs.ToList();
        }
    }
}
