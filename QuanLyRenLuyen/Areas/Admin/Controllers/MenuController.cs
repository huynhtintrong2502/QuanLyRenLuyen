using Model.Dao;
using Model.EF;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyRenLuyen.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ThongKe(User user1)
        {
            var NamHoc = new NamHocDao();
            ViewBag.ListNamHoc = NamHoc.ListAll();
            var HocKy = new HocKiDao();
            ViewBag.ListHocKy = HocKy.ListAll();
            var user = new UserDao().GetById(user1.UserName);
            return  View(user);
        }
        [HttpPost]
        public ActionResult ThongKe(string Id_NamHoc, string Id_HocKy)
        {
            var NamHoc = new NamHocDao();
            ViewBag.ListNamHoc = NamHoc.ListAll();
            var HocKy = new HocKiDao();
            ViewBag.ListHocKy = HocKy.ListAll();
            //Thống kê số lượng thành tích sinh viên
            if (Id_NamHoc == null || Id_NamHoc.Equals(""))
            {
                Id_NamHoc = "20192020";
            }
            if (Id_HocKy == null || Id_HocKy.Equals(""))
            {
                Id_HocKy = "1";
            }
            var diemRL = new DiemRenLuyenLopDao();
            int a = int.Parse(Id_NamHoc);
            int b = int.Parse(Id_HocKy);
            Session.Add("XS", diemRL.XS(a, b));
            Session.Add("Tot", diemRL.Tot(a, b));
            Session.Add("Kha", diemRL.Kha(a, b));
            Session.Add("TB", diemRL.TB(a, b));
            Session.Add("Yeu", diemRL.Yeu(a, b));
            Session.Add("Kem", diemRL.Kem(a, b));
            Session.Add("Gioi", diemRL.Tot(a, b));

            //Thống kê cơ cấu sinh viên hoàn thành
            var diem = new DiemRenLuyenDao();
            Session.Add("100", diem.TienDo100(a,b));
            Session.Add("80-99", diem.TienDo80_99(a, b));
            Session.Add("50-79", diem.TienDo50_79(a, b));
            Session.Add("0-49", diem.TienDo0_49(a, b));

            return View();
        }

        [HttpGet]
        public ActionResult NguoiDung(SinhVien SV)
        {
            SetViewBag(SV.Id_Lop);
            return View(SV);
        }

        public void SetViewBag(int? selectedId = null)
        {
            var dao = new LopDao();
            ViewBag.Id_Lop = new SelectList(dao.ListCurent(), "Id_Lop", "TenLop", selectedId);
        }
        public ActionResult Index(User user)
        {
            var SinhVien = new SinhVienDao();
            SinhVien.XoaSV(user);
            return View();
        }
        [HttpGet]
        public ActionResult DanhGia(SinhVien SV)
        {
            int a = int.Parse(Session["NamHoc"].ToString());
            int b = int.Parse(Session["HocKy"].ToString());
            
            var d = new DiemRenLuyenLopDao();
            int c =(int)SV.MaSV / 1000000000;
            if(c>0 && c<10)
                d.create(SV.MaSV, a, b);
            var diem = new DiemRenLuyenDao();

            if (d.GetByRL(SV.MaSV, a, b) == null)
            {
                d.create(SV.MaSV, a, b);
            }
            if (diem.GetByRL(SV.MaSV, a, b) == null)
            {
                diem.create(SV.MaSV, a, b);
            }


            ViewBag.DiemSV = diem.GetByRL(SV.MaSV, a, b);
            ViewBag.DiemSV_Lop = d.GetByRL(SV.MaSV, a, b);
            
            ViewBag.DiemTong = diem.DiemTong(SV.MaSV, a, b);
            ViewBag.DiemTong_Lop = d.DiemTong(SV.MaSV, a, b);
            return View(SV);
        }
        [HttpPost]
        public ActionResult DanhGia(double MaSV,int? Diem_1_1,int? Diem_1_2, int? Diem_1_3, int? Diem_1_4, int? Diem_2_1, int? Diem_2_2, int? Diem_2_3, int? Diem_3_1, int? Diem_3_2, int? Diem_4_1, int? Diem_4_2, int? Diem_4_3, int? Diem_5_1, int? Diem_5_2, int? Diem_5_3, int? Diem_6_1)
        {
            int a =  int.Parse(Session["NamHoc"].ToString());
            int b = int.Parse(Session["HocKy"].ToString());
            var diem2 = new DiemRenLuyenLopDao();
            diem2.Update(MaSV,a,b,Diem_1_1,Diem_1_2, Diem_1_3,  Diem_1_4,  Diem_2_1,  Diem_2_2,  Diem_2_3,  Diem_3_1,  Diem_3_2,  Diem_4_1, Diem_4_2, Diem_4_3, Diem_5_1, Diem_5_2,  Diem_5_3, Diem_6_1);

            var d = new DiemRenLuyenLopDao();
            var SV = new SinhVienDao().GetSV(MaSV);
            var diem = new DiemRenLuyenDao();
            ViewBag.DiemSV = diem.GetByRL(MaSV, a, b);
            ViewBag.DiemSV_Lop = d.GetByRL(MaSV, a, b); 
            ViewBag.DiemTong = diem.DiemTong(MaSV, a, b);
            ViewBag.DiemTong_Lop = d.DiemTong(MaSV, a, b);
            ModelState.AddModelError("", "Lưu Thành công.");
            var diemRL = new DiemRenLuyenLopDao();
            Session.Add("XS", diemRL.XS(20192020, 1));
            Session.Add("Tot", diemRL.Tot(20192020, 1));
            Session.Add("Kha", diemRL.Kha(20192020, 1));
            Session.Add("TB", diemRL.TB(20192020, 1));
            Session.Add("Yeu", diemRL.Yeu(20192020, 1));
            Session.Add("Kem", diemRL.Kem(20192020, 1));
            return View(SV);
        }
        [HttpGet]
        public ActionResult XuatFile()
        {
            var NamHoc = new NamHocDao();
            ViewBag.ListNamHoc = NamHoc.ListAll();
            var HocKy = new HocKiDao();
            ViewBag.ListHocKy = HocKy.ListAll();
            return View();
        }
        [HttpPost]
        public ActionResult XuatFile(string Id_NamHoc, string Id_HocKy)
        {
            var NamHoc = new NamHocDao();
            ViewBag.ListNamHoc = NamHoc.ListAll();
            var HocKy = new HocKiDao();
            ViewBag.ListHocKy = HocKy.ListAll();
            if (Id_NamHoc == null || Id_NamHoc.Equals(""))
            {
                Id_NamHoc = "20192020";
            }
            if (Id_HocKy == null || Id_HocKy.Equals(""))
            {
                Id_HocKy = "1";
            }
            int a = Int32.Parse(Id_NamHoc);
            int b = Int32.Parse(Id_HocKy);
            var List = new DiemRenLuyenLopDao().ListRenLuyen_Lop(a, b);

            
            if (List.Count() != 0)
            {
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet sheet = Ep.Workbook.Worksheets.Add("Report");
                sheet.Cells["A1"].Value = "Mã sinh viên";
                sheet.Cells["B1"].Value = "Họ và tên";
                sheet.Cells["C1"].Value = "Điểm mục 1.1";
                sheet.Cells["D1"].Value = "Điểm mục 1.2";
                sheet.Cells["E1"].Value = "Điểm mục 1.3";
                sheet.Cells["F1"].Value = "Điểm mục 1.4";
                sheet.Cells["G1"].Value = "Tổng mục 1";
                sheet.Cells["H1"].Value = "Điểm mục 2.1";
                sheet.Cells["I1"].Value = "Điểm mục 2.2";
                sheet.Cells["J1"].Value = "Điểm mục 2.3";
                sheet.Cells["K1"].Value = "Tổng mục 2";
                sheet.Cells["L1"].Value = "Điểm mục 3.1";
                sheet.Cells["M1"].Value = "Điểm mục 3.2";
                sheet.Cells["N1"].Value = "Tổng mục 3";
                sheet.Cells["O1"].Value = "Điểm mục 4.1";
                sheet.Cells["P1"].Value = "Điểm mục 4.2";
                sheet.Cells["Q1"].Value = "Điểm mục 4.3";
                sheet.Cells["R1"].Value = "Tổng mục 4";
                sheet.Cells["S1"].Value = "Điểm mục 5.1";
                sheet.Cells["T1"].Value = "Điểm mục 5.2";
                sheet.Cells["U1"].Value = "Điểm mục 5.3";
                sheet.Cells["V1"].Value = "Tổng mục 5";
                sheet.Cells["W1"].Value = "Điểm mục 6";
                sheet.Cells["X1"].Value = "Điểm tổng";
                int row = 2;
                foreach (var item in List)
                {
                    sheet.Cells[string.Format("A{0}", row)].Value = item.MaSV;
                    sheet.Cells[string.Format("B{0}", row)].Value = item.SinhVien.HoTen;
                    sheet.Cells[string.Format("C{0}", row)].Value = item.Muc1_1;
                    sheet.Cells[string.Format("D{0}", row)].Value = item.Muc1_2;
                    sheet.Cells[string.Format("E{0}", row)].Value = item.Muc1_3;
                    sheet.Cells[string.Format("F{0}", row)].Value = item.Muc1_4;
                    sheet.Cells[string.Format("G{0}", row)].Value = item.TongMuc1;
                    sheet.Cells[string.Format("H{0}", row)].Value = item.Muc2_1;
                    sheet.Cells[string.Format("I{0}", row)].Value = item.Muc2_2;
                    sheet.Cells[string.Format("J{0}", row)].Value = item.Muc2_3;
                    sheet.Cells[string.Format("K{0}", row)].Value = item.TongMuc2;
                    sheet.Cells[string.Format("L{0}", row)].Value = item.Muc3_1;
                    sheet.Cells[string.Format("M{0}", row)].Value = item.Muc3_2;
                    sheet.Cells[string.Format("N{0}", row)].Value = item.TongMuc3;
                    sheet.Cells[string.Format("O{0}", row)].Value = item.Muc4_1;
                    sheet.Cells[string.Format("P{0}", row)].Value = item.Muc4_2;
                    sheet.Cells[string.Format("Q{0}", row)].Value = item.Muc4_3;
                    sheet.Cells[string.Format("R{0}", row)].Value = item.TongMuc4;
                    sheet.Cells[string.Format("S{0}", row)].Value = item.Muc5_1;
                    sheet.Cells[string.Format("T{0}", row)].Value = item.Muc5_2;
                    sheet.Cells[string.Format("U{0}", row)].Value = item.Muc5_3;
                    sheet.Cells[string.Format("V{0}", row)].Value = item.TongMuc5;
                    sheet.Cells[string.Format("W{0}", row)].Value = item.Muc6_1;
                    sheet.Cells[string.Format("X{0}", row)].Value = item.TongMuc1 + item.TongMuc2 + item.TongMuc3 + item.TongMuc4 + item.TongMuc5 + item.Muc6_1;

                    row++;
                }
                
                sheet.Cells["A:AZ"].AutoFitColumns();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            else ModelState.AddModelError("", "Không tồn tại sinh viên.");


            return View();
        }


    }
}