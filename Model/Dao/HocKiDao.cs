using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class HocKiDao
    {

        QuanLyRenLuyenDbConText1 db = null;
        public HocKiDao()
        {
            db = new QuanLyRenLuyenDbConText1();
        }

        public List<HocKy> ListAll()
        {
            return db.HocKies.ToList();
        }

    }
}
