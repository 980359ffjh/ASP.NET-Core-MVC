using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RongProject1.Entities;
using RongProject1.ViewModels;
using System.Threading.Tasks;

namespace RongProject1.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManger;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManger,
                                 SignInManager<User> signInManager)
        {
            _userManger = userManger;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username };
                var createResult = await _userManger.CreateAsync(user, model.Password);

                //成功
                if(createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home"); /*跳到Index.cshtml*/
                }
                else
                {
                    //失敗
                    foreach (var error in createResult.Errors)
                    {
                        //跳出失敗的訊息
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResault = await _signInManager.PasswordSignInAsync(
                                            model.Username, model.Password,
                                            model.RememberMe, false);

                if (loginResault.Succeeded)
                {
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Could not login");

            return View();
        }
    }
}
