using Demo_Product.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo_Product.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserLoginViewModel p)
        {
            if (ModelState.IsValid)
            {
                var resault = await _signInManager.PasswordSignInAsync(p.UserName, p.Password, false, true);
                if (resault.Succeeded)
                {
                    return RedirectToAction("Index","Customer");
                }
                else
                {
                    ModelState.AddModelError("", "Hatalı kullanıcı adı ve şifre");
                }
            }
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Login");
        }

    }


}
//"Lockoutend" terimi, özellikle kurumsal bilgi teknolojisi (BT) sistemlerinde kullanılır.
//Bu terim, bir kullanıcının yanlış parola girişi veya başka bir güvenlik ihlali nedeniyle hesaplarının kilitlenmesi durumunda, bu kilitlenmenin sona erme zamanını belirtir.
//Örneğin, bir kullanıcı üç kez yanlış parola girerse hesabı kilitlenebilir ve bu kilitlenme belirli bir süre sonra otomatik olarak sona erebilir.
//Bu sürenin sonunda hesap tekrar erişilebilir hale gelir ve bu sona erme zamanı "lockoutend" olarak adlandırılır.

//"LockoutEnabled," genellikle kullanıcı hesaplarının güvenliğini sağlamak için kullanılan bir özelliktir.
//Bu özellik etkinleştirildiğinde, bir kullanıcı belirli sayıda başarısız oturum açma girişiminde bulunduğunda hesap kilitlenir.
//Bu, kötü niyetli kişilerin brute-force saldırıları yaparak hesap şifrelerini tahmin etmelerini zorlaştırır.


// acces fail count kullanıcının her hatalı şire girşinde  count değerini 1 arttırır toplam=5 tir 

// hatalı girişten sonra 5 dakika sisteme giremezsiniz .