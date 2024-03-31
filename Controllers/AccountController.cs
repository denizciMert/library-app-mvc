using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Data;
using LibraryApp.Models;
using BCrypt.Net;
using BCrypt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LibraryApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UsersModel usersModel;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        private bool IsUsernameTaken(string Adi)
        {
            return _context.kullanicilar.Any(k => k.KullaniciAdi == Adi);
        }

        private bool IsEmailTaken(string Eposta)
        {
            return _context.kullanicilar.Any(k => k.Eposta == Eposta);
        }

        private static bool IsPasswordComplex(string password)
        {
            var hasUpperCase = password.Any(char.IsUpper);
            var hasLowerCase = password.Any(char.IsLower);
            var hasNumbers = password.Any(char.IsDigit);
            var hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));
            var isValidLength = password.Length >= 8;

            return hasUpperCase && hasLowerCase && hasNumbers && hasSpecialChar && isValidLength;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("KullaniciID,Adi,Soyadi,Eposta,KullaniciAdi,Sifre")] RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                if (IsUsernameTaken(registerModel.KullaniciAdi) && IsEmailTaken(registerModel.Eposta))
                {
                    ModelState.AddModelError(string.Empty, "Girilen kullanıcı adı ve eposta kullanılıyor!");
                }
                else if(IsUsernameTaken(registerModel.KullaniciAdi))
                {
                    ModelState.AddModelError(string.Empty, "Girilen kullanıcı adı kullanılıyor!");
                }
                else if (IsEmailTaken(registerModel.Eposta))
                {
                    ModelState.AddModelError(string.Empty, "Girilen eposta kullanılıyor!");
                }
                else if (!IsPasswordComplex(registerModel.Sifre))
                {
                    ModelState.AddModelError(string.Empty, "Şifre en az 8 karakter uzunluğunda olmalı, büyük harf, küçük harf, rakam ve özel karakter içermelidir.");
                }
                else
                {
                    var salt = BCrypt.Net.BCrypt.GenerateSalt();
                    var usersModel = new UsersModel
                    {
                        KullaniciID = registerModel.KullaniciID,
                        Adi = registerModel.Adi,
                        Soyadi = registerModel.Soyadi,
                        Eposta = registerModel.Eposta,
                        KullaniciAdi = registerModel.KullaniciAdi,
                        Sifre = BCrypt.Net.BCrypt.HashPassword(registerModel.Sifre, salt),
                        Rol = "Kullanıcı"
                    };
                    _context.Add(usersModel);
                    try 
                    {
                        await _context.SaveChangesAsync();
                        ModelState.AddModelError(string.Empty, "Kaydınız başarıyla oluşturuldu.");
                        return View(registerModel);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        ModelState.AddModelError(string.Empty, "Kaydınız oluşturulurken bir hata meydana geldi. Bir süre sonra tekrar deneyin veya sistem yöneticisi ile iletişime geçin.");
                        return View(registerModel);
                    }
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kaydınız oluşturulurken bir hata meydana geldi. Bir süre sonra tekrar deneyin veya sistem yöneticisi ile iletişime geçin.");
            }
            return View(registerModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("IdentityInfo,PasswordInfo")] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var loggedUser = await _context.kullanicilar.FirstOrDefaultAsync(p => p.KullaniciAdi == loginModel.IdentityInfo);
                if (loggedUser != null && BCrypt.Net.BCrypt.Verify(loginModel.PasswordInfo, loggedUser.Sifre))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, loggedUser.KullaniciAdi),
                        new Claim(ClaimTypes.Role, loggedUser.Rol),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    ViewBag.isUserLoggedIn = "Giriş İşlemi Başarılı! Yönlendiriliyor...";
                    return RedirectToAction("HomePage", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı.");
                    return View(loginModel);
                }
            }
            return View(loginModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
        private bool UsersModelExists(int id)
        {
            return _context.kullanicilar.Any(e => e.KullaniciID == id);
        }
    }
}
