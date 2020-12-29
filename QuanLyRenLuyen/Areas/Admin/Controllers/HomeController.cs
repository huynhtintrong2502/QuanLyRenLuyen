using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;

namespace QuanLyRenLuyen.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        [HttpGet]
        public ActionResult Index(User us)
        {
            if(us != null)
            {
                var SinhVien = new SinhVienDao();
                SinhVien.XoaSV(us);
            }
            var SinhVienDao = new SinhVienDao();
            ViewBag.ListSinhVien = SinhVienDao.ListSinhVien();
            var NamHoc = new NamHocDao();
            ViewBag.ListNamHoc = NamHoc.ListAll();
            var HocKy = new HocKiDao();
            ViewBag.ListHocKy = HocKy.ListAll();
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Id_NamHoc, string Id_HocKy, string search_text)
        {
            
            if (Id_NamHoc == null || Id_NamHoc.Equals(""))
            {
                Session.Add("NamHoc", "20192020");
            }
            else Session.Add("NamHoc", Id_NamHoc);
            if (Id_HocKy == null || Id_HocKy.Equals(""))
            {
                Session.Add("HocKy", "1");
            }else Session.Add("HocKy", Id_HocKy);



            var SinhVienDao = new SinhVienDao();
            ViewBag.ListSinhVien = SinhVienDao.ListSinhVienSearch(search_text);
            var NamHoc = new NamHocDao();
            ViewBag.ListNamHoc = NamHoc.ListAll();
            var HocKy = new HocKiDao();
            ViewBag.ListHocKy = HocKy.ListAll();
            return View();
        }

       

    }
}