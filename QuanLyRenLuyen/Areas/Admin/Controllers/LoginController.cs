using Facebook;
using Model.Dao;
using Model.EF;
using QuanLyRenLuyen.Common;
using QuanLyRenLuyen.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuanLyRenLuyen.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        // GET: Admin/Login
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, model.PassWord);

                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.MaSV = user.MaSV;
                    Session.Add(CommonConstans.USER_SESSION, userSession);
                    Session.Add("MaSV", user.MaSV);


                    Session.Add("NamHoc", "20192020");
                    Session.Add("HocKy", "1");

                    var diemRL = new DiemRenLuyenLopDao();
                    Session.Add("XS", diemRL.XS(20192020, 1));
                    Session.Add("Tot", diemRL.Tot(20192020, 1));
                    Session.Add("Kha", diemRL.Kha(20192020, 1));
                    Session.Add("TB", diemRL.TB(20192020, 1));
                    Session.Add("Yeu", diemRL.Yeu(20192020, 1));
                    Session.Add("Kem", diemRL.Kem(20192020, 1));

                    var diem = new DiemRenLuyenDao();
                    Session.Add("100", diem.TienDo100(20192020, 1));
                    Session.Add("80-99", diem.TienDo80_99(20192020, 1));
                    Session.Add("50-79", diem.TienDo50_79(20192020, 1));
                    Session.Add("0-49", diem.TienDo0_49(20192020, 1));



                    return RedirectToAction("Index", "Home");
                }
                else if (result == 2)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.MaSV = user.MaSV;
                    Session.Add(CommonConstans.USER_SESSION, userSession);
                    Session.Add("MaSV", user.MaSV);
                    Session.Add("NamHoc", "20192020");
                    Session.Add("HocKy", "1");

                    var sv = new SinhVienDao().GetSV(user.MaSV);
                    Session.Add("Anh", sv.Anh);
                    Session.Add("HoTen", sv.HoTen);
                    return RedirectToAction("Account", "SinhVien");
                }
                else if (result == 0)
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                else ModelState.AddModelError("", "Mật khẩu không đúng.");

            }
            return View("Index");

        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(double MaSV, string UserName, string Password,string HoTen)
        {
            var user = new UserDao().create(MaSV, UserName, Password);
            if(user == 0)
            {
                ModelState.AddModelError("", "Tài khoản đã tồn tại.");
            }
            else
            {
                var sv = new SinhVienDao();
                sv.create(MaSV, HoTen);
                ModelState.AddModelError("", "Bạn đã tạo thành công.");
            }
            return View();
        }



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Login");
        }


        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
        
                string email1 = me.email;

                var dao = new UserDao();

                var result1 = dao.Login(email1, email1);

                if (result1 == 1)
                {
                    var user1 = dao.GetById(me.email);
                    var userSession = new UserLogin();
                    userSession.UserName = user1.UserName;
                    userSession.MaSV = user1.MaSV;
                    Session.Add(CommonConstans.USER_SESSION, userSession);
                    Session.Add("MaSV", user1.MaSV);


                    Session.Add("NamHoc", "20192020");
                    Session.Add("HocKy", "1");

                    var diemRL = new DiemRenLuyenLopDao();
                    Session.Add("XS", diemRL.XS(20192020, 1));
                    Session.Add("Tot", diemRL.Tot(20192020, 1));
                    Session.Add("Kha", diemRL.Kha(20192020, 1));
                    Session.Add("TB", diemRL.TB(20192020, 1));
                    Session.Add("Yeu", diemRL.Yeu(20192020, 1));
                    Session.Add("Kem", diemRL.Kem(20192020, 1));

                    var diem = new DiemRenLuyenDao();
                    Session.Add("100", diem.TienDo100(20192020, 1));
                    Session.Add("80-99", diem.TienDo80_99(20192020, 1));
                    Session.Add("50-79", diem.TienDo50_79(20192020, 1));
                    Session.Add("0-49", diem.TienDo0_49(20192020, 1));
                  

                }
                else if (result1 == 2)
                {
                    var user1 = dao.GetById(me.email);
                    var userSession = new UserLogin();
                    userSession.UserName = user1.UserName;
                    userSession.MaSV = user1.MaSV;
                    Session.Add(CommonConstans.USER_SESSION, userSession);
                    Session.Add("MaSV", user1.MaSV);
                    Session.Add("NamHoc", "20192020");
                    Session.Add("HocKy", "1");

                    var sv = new SinhVienDao().GetSV(user1.MaSV);
                    Session.Add("Anh", sv.Anh);
                    Session.Add("HoTen", sv.HoTen);
                    //return RedirectToAction("DanhGia", "SinhVien");
                    return Redirect("~/areas/Admin/Controller/SinhVien/DanhGia");

                }
                else
                {
                    string id = me.id;
                    string email = me.email;
                    string firstname = me.first_name;
                    string middlename = me.middle_name;
                    string lastname = me.last_name;

                    var user = new User();
                    user.UserName = email;
                    user.PassWords = email;
                    user.MaSV = double.Parse(id);
                    user.LoaiDangNhap = 2;
                    user.xoa = 0;

                    var userdao = new UserDao();
                    userdao.Insert(user);

                    var SV = new SinhVien();
                    SV.MaSV = double.Parse(id);
                    SV.HoTen = firstname + " " + middlename + " " + lastname;
                    SV.Email = me.email;
                    SV.Anh = "/assets/client/image/anhmacdinh.jpg";
                    SV.xoa = 0;

                    var SVDao = new SinhVienDao();
                    SVDao.Insert(SV);

                    var user1 = dao.GetById(email);
                    var userSession = new UserLogin();
                    userSession.UserName = user1.UserName;
                    userSession.MaSV = user1.MaSV;
                    Session.Add(CommonConstans.USER_SESSION, userSession);
                    Session.Add("MaSV", user1.MaSV);
                    Session.Add("NamHoc", "20192020");
                    Session.Add("HocKy", "1");

                    var sv = new SinhVienDao().GetSV(user1.MaSV);
                    Session.Add("Anh", sv.Anh);
                    Session.Add("HoTen", sv.HoTen);
                    return RedirectToAction("DanhGia", "SinhVien");
                }

            }
            return Redirect("/");
        }



    }
}