using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FabrikaYonetimSistemi.Web.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<Personnel> _userManager;
        private readonly SignInManager<Personnel> _signInManager;

        public AccountController(UserManager<Personnel> userManager, SignInManager<Personnel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("login")]
        public IActionResult Login() => View();
        

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                /*var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    Console.WriteLine($"Login failed: No user found with email {model.Email}");
                    ModelState.AddModelError(string.Empty, "User not found.");
                    return View(model);
                }

                else
                {
                    Console.WriteLine($"User found: {user.Email}");
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!isPasswordValid)
                {
                    Console.WriteLine($"Login failed: Incorrect password for user {model.Email}");
                    ModelState.AddModelError(string.Empty, "Invalid password.");
                    return View(model);
                }

                else
                {
                    Console.WriteLine($"Password valid for user {model.Email}");
                }

                if (await _userManager.IsLockedOutAsync(user))
                {
                    Console.WriteLine($"Login failed: User {model.Email} is locked out.");
                    ModelState.AddModelError(string.Empty, "Account is locked.");
                    return View(model);
                }

                else
                {
                    Console.WriteLine($"User {model.Email} is not locked out.");
                }

                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    Console.WriteLine($"Login failed: User {model.Email} has not confirmed their email.");
                    ModelState.AddModelError(string.Empty, "Please confirm your email before logging in.");
                    return View(model);
                }

                else
                {
                    Console.WriteLine($"Email confirmed for user {model.Email}");
                }

                var roles = await _userManager.GetRolesAsync(user);
                Console.WriteLine($"Roles for user {model.Email}: {string.Join(", ", roles)}");
                if (!roles.Any())
                {
                    Console.WriteLine($"User {model.Email} has no roles assigned.");
                    ModelState.AddModelError(string.Empty, "No roles assigned to the user.");
                    return View(model);
                }

                if (!result.Succeeded)
                {
                    Console.WriteLine($"SignInResult details: {Newtonsoft.Json.JsonConvert.SerializeObject(result)}");

                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Account is locked.");
                    }
                    else if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError(string.Empty, "Login is not allowed for this user.");
                    }
                    else if (result.RequiresTwoFactor)
                    {
                        ModelState.AddModelError(string.Empty, "Two-factor authentication is required.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }

                    return View(model);
                }*/

                ModelState.AddModelError(string.Empty, "Şifre veya e-posta hatalı");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("register")]
        public IActionResult Register() => View();

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Personnel
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, "Personel");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                Console.WriteLine("[Register] ModelState is invalid.");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"[Register] ModelState error: {error.ErrorMessage}");
                }
            }
            Console.WriteLine("[Register] Returning to registration view.");
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet("accessdenied")]
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}
