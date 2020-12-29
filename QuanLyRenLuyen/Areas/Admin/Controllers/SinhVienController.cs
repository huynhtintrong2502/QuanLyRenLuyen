using Elmah.ContentSyndication;
using Model.Dao;
using Model.EF;
using QuanLyRenLuyen.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyRenLuyen.Areas.Admin.Controllers
{
    public class SinhVienController : BaseController
    {
        // GET: Admin/SinhVien
        [HttpGet]
        public ActionResult Index() {
            return View();
        }

        [HttpGet]
        public ActionResult DanhGia()
        {
            string NamHoc = Session["NamHoc"].ToString();
            string HocKy = Session["HocKy"].ToString();
            ViewBag.DD = 1;


            string a = Session["MASV"].ToString();
            ViewBag.SV = new SinhVienDao().GetSV(Double.Parse(a));
            var DiemSV = new DiemRenLuyenDao();
            if(DiemSV.GetByRL(Double.Parse(a), int.Parse(NamHoc), int.Parse(HocKy)) == null)
            {
                DiemSV.create(Double.Parse(a), int.Parse(NamHoc), int.Parse(HocKy));
            }
            ViewBag.DiemSV = DiemSV.GetByRL(Double.Parse(a), int.Parse(NamHoc), int.Parse(HocKy));
            return View();
        }
        [HttpPost]
        public ActionResult DanhGia(double MaSV, int? Diem_1_1, int? Diem_1_2, int? Diem_1_3, int? Diem_1_4, int? Diem_2_1, int? Diem_2_2, int? Diem_2_3, int? Diem_3_1, int? Diem_3_2, int? Diem_4_1, int? Diem_4_2, int? Diem_4_3, int? Diem_5_1, int? Diem_5_2, int? Diem_5_3, int? Diem_6_1,string GhiChu1_2, string GhiChu1_3, string GhiChu3_1, double DiemHKTruoc, double DiemHKNay)
        {
            int a = int.Parse(Session["NamHoc"].ToString());
            int b = int.Parse(Session["HocKy"].ToString());
            var diem2 = new DiemRenLuyenDao();
            
            diem2.Update(MaSV, a, b, Diem_1_1, Diem_1_2, Diem_1_3, Diem_1_4, Diem_2_1, Diem_2_2, Diem_2_3, Diem_3_1, Diem_3_2, Diem_4_1, Diem_4_2, Diem_4_3, Diem_5_1, Diem_5_2, Diem_5_3, Diem_6_1,GhiChu1_2,GhiChu1_3,GhiChu3_1, DiemHKTruoc, DiemHKNay);
            
            var SV = new SinhVienDao().GetSV(MaSV);
            var diem = new DiemRenLuyenDao();
            ViewBag.DiemSV = diem.GetByRL(MaSV, a, b);
            ViewBag.DiemTong = diem.DiemTong(MaSV, a, b);

            ModelState.AddModelError("", "Lưu Thành công.");
            return View(SV);
        }

        [HttpGet]
        public ActionResult DanhGia1()
        {
            var NamHoc = new NamHocDao();
            ViewBag.ListNamHoc = NamHoc.ListAll();
            var HocKy = new HocKiDao();
            ViewBag.ListHocKy = HocKy.ListAll();
            return View();
        }
        [HttpPost]
        public ActionResult DanhGia1(string Id_NamHoc, string Id_HocKy)
        {
            if (Id_NamHoc == "" || Id_NamHoc == null)
                Id_NamHoc = "20192020";
            if (Id_HocKy == "" || Id_HocKy == null)
                Id_HocKy = "1";
            Session.Add("NamHoc", Id_NamHoc);
            Session.Add("HocKy", Id_HocKy);
            return RedirectToAction("DanhGia", "SinhVien");
        }





        [HttpGet]
        public ActionResult Account()
        {

            string a = Session["MASV"].ToString();
            var sv = new SinhVienDao().GetSV(double.Parse(a));
            SetViewBag(sv.Id_Lop);
            return View(sv);
        }
        [HttpPost]
        public ActionResult Account(SinhVien model, DateTime? ngaySinh)
        {

            string a = Session["MASV"].ToString();
            var sv = new SinhVienDao().GetSV(double.Parse(a));
            var SinhVien = new SinhVienDao();
            SinhVien.Update(model, ngaySinh);
            ModelState.AddModelError("", "Cập nhật Thành công.");
            SetViewBag(sv.Id_Lop);
            Session.Add("MaSV", model.MaSV);
            return View(sv);
        }
        public void SetViewBag(int? selectedId = null)
        {
            var dao = new LopDao();
            ViewBag.Id_Lop = new SelectList(dao.ListAll(), "Id_Lop", "TenLop", selectedId);
        }

        
    }
}