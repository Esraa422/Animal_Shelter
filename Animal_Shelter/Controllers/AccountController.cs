using Animal_Shelter.Data;
using Animal_Shelter.Models;
using Animal_Shelter.Data;
using Animal_Shelter.Data.Services;
using Animal_Shelter.Data.Static;
using Animal_Shelter.Data.ViewModels;
using Animal_Shelter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Animal_Shelter.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userM;
        private readonly SignInManager<ApplicationUser> _sinInM;

        private readonly AppDbContext _context;
        public AccountController(UserManager<ApplicationUser> userM, SignInManager<ApplicationUser> sinInM, AppDbContext context)
        {
            _userM = userM;
            _sinInM = sinInM;
            _context = context;
        }
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)

        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userM.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userM.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _sinInM.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {

                        return RedirectToAction("Index", "Animals");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }
        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userM.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var newUser = new ApplicationUser
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };
            var newUserResponse = await _userM.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                TempData["success"] = "Registration is Successful";
                await _userM.AddToRoleAsync(newUser, UserRoles.User);

                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (var error in newUserResponse.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _sinInM.SignOutAsync();
            return RedirectToAction("Index", "Animals");
        }
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            return LocalRedirect(returnUrl);
        }
    }
}