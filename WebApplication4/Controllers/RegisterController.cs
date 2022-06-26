using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication4.Models;
using WebApplication4.Vm;

namespace WebApplication4.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _user;
        private readonly SignInManager<AppUser> _sign;

        public RegisterController(SignInManager<AppUser> sign, UserManager<AppUser> user)
        {
            _user = user;
            _sign = sign;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = new AppUser
            {
                UserName = registerVm.UserName,
                FirstName = registerVm.FirstName,
                LastName = registerVm.LastName,
                Email = registerVm.Email,



            };
            IdentityResult result = await _user.CreateAsync(appUser, registerVm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);

                }

            }
            await _sign.SignInAsync(appUser, true);



            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut(RegisterVm registerVm)
        {
            await _sign.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            AppUser appUser;
            if (loginVm.EmailOrUserName.Contains("@"))
            {
                appUser = await _user.FindByEmailAsync(loginVm.EmailOrUserName);


            }
            else
            {
                appUser = await _user.FindByNameAsync(loginVm.EmailOrUserName);
            }
            if (appUser == null)
            {
                ModelState.AddModelError("", "Email ve ya istifadeci adini daxil edin");
                return View(loginVm);
            }
            var musi = await _sign.PasswordSignInAsync(appUser,loginVm.Password, loginVm.RememberMe, true);


            if (musi.IsLockedOut)
            {
                ModelState.AddModelError("", "Coxlu giris cehtdi bas verib. get biraz sora gel");
                return View(loginVm);
            }

            if(!musi.Succeeded)
            {
                ModelState.AddModelError("", "parol ve ya username sefdir");
                return View(loginVm);

            }
            return RedirectToAction("Index","Home");

        }
    }
}
