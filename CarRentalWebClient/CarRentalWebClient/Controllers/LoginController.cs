using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace CarRentalWebClient.Controllers
{
    public class LoginController : SiteController
    {
        // GET: DangNhap
        // CHỨC NĂNG ĐĂNG NHẬP
      
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f, string returnUrl)
        {
            if (ModelState.IsValid)
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
                    var kh = (from t in Db.Users where t.userName == username && t.password == password select t).ToList();

                    bool userValid = kh.Any();
                    //bool userValid = entities.KhachHangs.Any(user => user.TenDangNhap == username && user.MatKhau == password);
                    // User tìm trong database
                    if (userValid)
                    {
                        foreach (var a in kh)
                        {
                            if (a.Admin == false)
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
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                        }



                    }
                    else
                    {

                        return RedirectToAction("Index", "Home");

                    }



                }
            
            return View(f);
        }
        public ActionResult LogOff()
        {
            Session["Account"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
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
            try
            {
                var usr = (from kh in Db.Users where (kh.email == Email) select kh).ToList();
                string pwd = "";
                string usrname = "";
                string chuoi = "";
                foreach (CarRentalServiceReference.User k in usr)
                {
                    if (k.Admin == false)
                    {
                        pwd = k.password;
                        usrname = k.userName;
                    }
                    else
                    {
                        ViewBag.Error1 = "<script language=javascript>alert('Xin lỗi Email này tại trong dữ liệu!');</script>";
                    }
                   
                }

                chuoi += "Tên đăng nhập:" + usrname + " ";
                chuoi += "\n Mật khẩu: " + pwd + " ";

                string mail = "Chào Email: " + Email + chuoi;
                SendEmail(Email, "Car Store", mail);

                return RedirectToAction("Login", "Login");
            }
            catch
            {

                ViewBag.Error = "<script language=javascript>alert('Nhập lại mật khẩu');</script>";
                return RedirectToAction("ForgotPassword", "Login");
            }
            //KhachHang usr = db.KhachHangs.SingleOrDefault(u => u.Email == Email);

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
    }
}