using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StackExchange.Redis;

namespace FabrikaYonetimSistemi.Web.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<Personnel> _userManager;
        private readonly SignInManager<Personnel> _signInManager;
        private const int MaxFailedAttempts = 5;
        private const int LockoutMinutes = 5;
        private readonly IDatabase _redisDb;
        
        public AccountController(UserManager<Personnel> userManager, SignInManager<Personnel> signInManager, IDatabase redisDb)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _redisDb = redisDb;
        }

        [HttpGet("Login")]
        public IActionResult Login() => View();

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            /*if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("");
                }

                ModelState.AddModelError(string.Empty, "Şifre veya e-posta hatalı");
            }
            return View(model);*/
            
            if(!ModelState.IsValid) return View(model);
            
            string redisKey = $"failed_attempts:{model.Email}";
            int failedAttempts = (int?)await _redisDb.StringGetAsync(redisKey) ?? 0;

            //Kullanıcı çok fazla başarısız giriş yaptıysa engelle
            if (failedAttempts >= MaxFailedAttempts)
            {
                ModelState.AddModelError("", $"Çok fazla başarısız giriş denemesi yapıldı. Lütfen {LockoutMinutes} dakika bekleyin.");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            
            if (result.Succeeded)
            {
                await _redisDb.KeyDeleteAsync(redisKey); // Başarılı girişte sayaçı sıfırla
                return RedirectToAction("Index", "Home");
            }
            
            // Başarısız giriş, sayacı artır
            failedAttempts++;
            await _redisDb.StringSetAsync(redisKey, failedAttempts, TimeSpan.FromMinutes(LockoutMinutes));
            
            ModelState.AddModelError("", "Şifre veya e-posta hatalı.");
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("RegisterAdmin")]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost("Logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Oturumu sonlandır
            await _signInManager.SignOutAsync();

            // Giriş sayfasına yönlendir
            return RedirectToAction("Login");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = new Personnel
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname
                };

                var result = await _userManager.CreateAsync(admin, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "Admin");
                    return RedirectToAction("");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("RegisterPersonnel")]
        public IActionResult RegisterPersonnel()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("RegisterPersonnel")]
        public async Task<IActionResult> RegisterPersonnel(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var personnel = new Personnel
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname
                };

                var result = await _userManager.CreateAsync(personnel, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(personnel, "Personel");
                    return RedirectToAction("");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("");
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet("Accessdenied")]
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}
