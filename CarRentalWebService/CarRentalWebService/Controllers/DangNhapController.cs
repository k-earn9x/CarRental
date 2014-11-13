using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRentalWebService.Models;
using System.Web.Security;
using System.Net;
using System.Net.Mail;

namespace CarRentalWebService.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: DangNhap
        // CHỨC NĂNG ĐĂNG NHẬP
        DbContextModel db = new DbContextModel();
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (DbContextModel entities = new DbContextModel())
                {
                    string username = f["username"];
                    string password = f["txtpass"];
                    string _remember = f["remember"];
                    bool remember;
                    if (_remember == null)
                    {
                        remember = false;
                    }
                    else
                    {
                        remember = true;
                    }
                    var kh = (from t in entities.Users where t.userName == username && t.password == password select t).ToList();

                    bool userValid = kh.Any();
                    //bool userValid = entities.KhachHangs.Any(user => user.TenDangNhap == username && user.MatKhau == password);
                    // User tìm trong database
                    if (userValid)
                    {

                        Session["Account"] = username;
                        FormsAuthentication.SetAuthCookie(username, remember);

                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                             && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            //ViewBag.Err = "<script language=javascript>alert('Sai thông tin đăng nhập!');</script>";                          
                            return RedirectToAction("Index", "DashBoard");
                        }

                    }
                    else
                    {

                        return RedirectToAction("Index", "DashBoard");

                    }



                }
            }
            return View(f);
        }
        public ActionResult LogOff()
        {
            Session["Account"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("DangNhap", "DangNhap");
        }
        //CHỨC NĂNG QUÊN MẬT KHẨU
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(FormCollection f)
        {
            string Email = f["email"];

            //KhachHang usr = db.KhachHangs.SingleOrDefault(u => u.Email == Email);
            var usr = (from kh in db.Users where (kh.email == Email) select kh).ToList();
            string pwd = "";
            string usrname = "";
            string chuoi = "";
            foreach (User k in usr)
            {
                pwd = k.password;
                usrname = k.userName;
            }

            chuoi += "Tên đăng nhập:" + usrname + " ";
            chuoi += "\n Mật khẩu: " + pwd + " ";

            string mail = "Chào Email: " + Email + chuoi;
            SendEmail(Email, "Books Shop", mail);

            return RedirectToAction("DangNhap", "DangNhap");
        }
        public void SendEmail(string address, string subject, string message)
        {
            string email = "booksshop2204@gmail.com";
            string password = "nguyenhoangnam";

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(address));
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }
        // CHỨC NĂNG ĐĂNG KÝ
        [HttpGet]
        public ActionResult FormRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FormRegister(FormCollection f)
        {
            string pass = f["pass"];
            string pass2 = f["pass2"];

            User usr = new User();



            usr.userName = f["name"];
            usr.password = f["pass"];
            usr.Name = f["name2"];
            usr.email = f["email"];


            if (ModelState.IsValid)
            {
                using (DbContextModel entities = new DbContextModel())
                {
                    string name = f["name"];
                    bool userInValid = entities.Users.Any(user => user.userName == name);
                    if (userInValid && f["name"] != null)
                    {

                        ViewBag.Error = "<script language=javascript>alert('Đăng kí không thành công Tên đăng nhập bị trùng');</script>";




                        //ViewBag.Mess = "Tên đăng nhập đã tồn tại!";
                    }

                    else if (pass != pass2 && pass2 != null && pass != null)
                    {
                        ViewBag.ErrorPass = "<script language=javascript>alert('Mật khẩu không trùng khớp');</script>";
                    }


                    else if (f["agree"] == null)
                    {
                        ViewBag.ErrorAgree = "<script language=javascript>alert('Bạn chưa đồng ý với điều kiện của chúng tôi!');</script>";
                    }
                    else
                    {


                        //ViewBag.success = "<script language=javascript>alert('Đăng kí thành công!');</script>";
                        //db.KhachHangs.AddObject(usr);
                        entities.Users.Add(usr);
                        entities.SaveChanges();
                        return RedirectToAction("DangNhap", "DangNhap");

                    }
                }

            }

            return View(usr);
        }
    }
}