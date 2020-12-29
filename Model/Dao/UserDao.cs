using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Dao;
using Model.EF;

namespace Model.Dao
{
    public class UserDao
    {
        QuanLyRenLuyenDbConText1 db = null;
        public UserDao()
        {
            db = new QuanLyRenLuyenDbConText1();
        }
        public void Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
        }
        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public void update(double maSV1,double maSV2)
        {
            var us = db.Users.SingleOrDefault(x => x.MaSV == maSV1);
            us.MaSV = maSV2;
            db.SaveChanges();
        }

        public int? Login(string UserName, string PassWord)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == UserName);
            if(result == null )
            {
                return 0;
            }
            else
            {
                if (result.PassWords == PassWord)
                {
                    return result.LoaiDangNhap;
                }
                else return -1;
            }
        }

        public int create(double MaSV,string UserName,string Password)
        {
            var user = db.Users.SingleOrDefault(x => x.MaSV == MaSV || x.UserName == UserName);
            if (user != null) return 0;
            else
            {
                User us = new User();
                us.MaSV = MaSV;
                us.UserName = UserName;
                us.PassWords = Password;
                us.LoaiDangNhap = 2;
                us.xoa = 0;
                Insert(us);
                return 1;
            }
        }
    }
}
