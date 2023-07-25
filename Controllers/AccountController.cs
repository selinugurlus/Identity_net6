using blog.Identity;
using blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace blog.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        private SignInManager<ApplicationUser> _signinManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signinManager

           )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signinManager = signinManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = register.Email,
                    UserName = register.Username,
                    Name = register.Name,
                    Surname = register.Surname

                };

                try
                {
                    IdentityResult result = await _userManager.CreateAsync(user, register.Password);

                    await _userManager.AddToRoleAsync(user, "Moderator");

                    if (result.Succeeded)
                    {
                       
                        return RedirectToAction("Login");
                    }

                 
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(register);
                    }
                }
                catch (Exception ex)
                {
                    // Hata mesajını konsol
                    Console.WriteLine("Hata Oluştu: " + ex.Message);
                    // Hatanın View'e gösterilmesi
                    ModelState.AddModelError("", "Kayıt işlemi sırasında bir hata oluştu.");
                    return View(register);
                }
            }
            return View(register);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Login user)
        {

            try
            {
                if (ModelState.IsValid)
                {
                  
                    var result = await _signinManager.PasswordSignInAsync(user.Username, user.Password, false, false);
                    if (result.Succeeded)
                    { 
                        return RedirectToAction("UsersIndex","Blog");
                    }
                   
                }

                return View();
            }
            catch (Exception ex)
            {
                // Hata mesajını konsola yazdır.
                Console.WriteLine("Hata Oluştu: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                // Hatanın View'e gösterilmesi
                ModelState.AddModelError("", "Giriş işlemi sırasında bir hata oluştu.");
                return View();
            }

           
        }
    }
}



