using Microsoft.AspNetCore.Mvc;
using Promote.website.Models;
using Promote.website.Services;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MimeKit;
using System;
using Microsoft.AspNetCore.Identity;
using StajyerTakipSistemi.Web;
using Microsoft.EntityFrameworkCore;

namespace Promote.website.Controllers
{
    public class LoginController : Controller
    {
        private readonly Context _context;
        private readonly LayoutService _layoutService;
        public LoginController(Context context, LayoutService layoutService)
        {
            _context = context;
            _layoutService = layoutService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(Admin p)
        { 
            var adminuserinfo = _context.admins?.FirstOrDefault(x => x.UserName == p.UserName && x.Password == PasswordHasher.HashPassword(p.Password));

            if (adminuserinfo != null)
            {
                HttpContext.Session.SetString("UserId", adminuserinfo.AdminId.ToString());//HttpContext.Session.GetString("UserId")
                return RedirectToAction("Router", "AboutPages");

            }
            else
            {
                TempData["Message"] = "Hatalı giriş!";
                return View("Index");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public bool SendEmail(string from, string to, string subject, string content)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("", from));
                message.To.Add(new MailboxAddress("", to));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = content;
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("stajyertakip@gmail.com", "aircjpwffhjocewl");

                    client.Send(message);
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine("E-posta gönderme hatası: " + ex.Message);
                return false;
            }
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost] 
        public IActionResult CodeSend(string UserName,string Email)
        {
            if (Email == null || UserName == null)
            {
                TempData["Message"] = "İstenen bilgileri doldurunuz.";
                TempData["AlertClass"] = "alert-danger";
                return RedirectToAction("Index");
            }
            var Admin = _context.admins.FirstOrDefault(s => s.Email == Email && s.UserName == UserName);
             

            if (Admin != null)
            {
                var token = Guid.NewGuid().ToString();
                var expirationTime = DateTime.UtcNow.AddHours(1);
                var tokenData = new PasswordResetToken
                {
                    AdminId = Admin.AdminId,
                    Token = token,
                    ExpirationTime = expirationTime
                };

                _context.passwordResetTokens.Add(tokenData);
                _context.SaveChanges();

                var from = "stajyertakip@gmail.com";
                var to = Email;
                var subject = "Şifreni Sıfırla 👾";
                var content = $"Şifreni sıfırlamak için bu linke tıkla: https://localhost:7237/Login/ChangePassword?token={token}";

                // E-posta gönderme işlemi
                bool emailSent = SendEmail(from, to, subject, content);

                if (emailSent)
                {
                    ViewBag.EmailSent = true;
                    TempData["Message"] = "Email başarıyla gönderildi..";
                    TempData["AlertClass"] = "alert-success";
                }
                else
                {
                    ViewBag.EmailSent = false;
                    TempData["Message"] = "Kullanıcı bulunamadı veya email gönderilemedi.";
                    TempData["AlertClass"] = "alert-danger";

                }
            } 
            else
            {
                TempData["Message"] = "Kullanıcı bulunamadı veya email gönderilemedi.";
                TempData["AlertClass"] = "alert-danger";
                return View("ForgotPassword");
            }
            
              
            return View("Index");
        }
        [HttpGet]
        public IActionResult ChangePassword(string token)
        {
            if (string.IsNullOrEmpty(token))
            {

                TempData["Message"] = "Geçersiz istek!";
                TempData["AlertClass"] = "alert-danger";
                return RedirectToAction("Index", "Login");
            }
            var tokenData = _context.passwordResetTokens.FirstOrDefault(t => t.Token == token);
            if (tokenData != null && tokenData.ExpirationTime > DateTime.UtcNow)
            {
                ViewBag.AdminId = tokenData.AdminId;
                ViewBag.Token = token;
                return View("ChangePassword");
            }

            TempData["Message"] = "Geçersiz veya süresi dolmuş belirteç!";
            TempData["AlertClass"] = "alert-danger";
            return RedirectToAction("Index", "Login");

        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(int adminId, string newPassword, string token)
        {
            if (adminId==null || string.IsNullOrEmpty(newPassword))
            {

                TempData["Message"] = "Geçersiz istek!";
                TempData["AlertClass"] = "alert-danger";
                return RedirectToAction("ChangePassword", new { token });//////////
            }
            if (newPassword.Length < 8)
            {
                TempData["Message"] = "Şifreniz en az 8 karakter içermelidir!";
                TempData["AlertClass"] = "alert-danger";
                return RedirectToAction("ChangePassword", new { token });////////
            }

             
                 
                var Admin = await _context.admins.FirstOrDefaultAsync(s => s.AdminId == adminId);

                if (Admin != null)
                {

                    Admin.Password = PasswordHasher.HashPassword(newPassword);
                    _context.Update(Admin);
                }
                else
                {

                    TempData["Message"] = "Geçersiz istek!";
                    TempData["AlertClass"] = "alert-danger";
                    return RedirectToAction("ChangePassword", adminId);
                }

                await _context.SaveChangesAsync();

                // Şifre sıfırlama işlemi başarılı 
                TempData["Message"] = "Şifre başarıyla sıfırlandı!";
                TempData["AlertClass"] = "alert-success";
                return RedirectToAction("Index");//return RedirectToAction("ChangePassword", new { guid });
           
             
        }

    }
}
