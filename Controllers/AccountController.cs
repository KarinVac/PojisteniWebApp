using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PojisteniWebApp.Controllers;
using PojisteniWebApp.Data;
using PojisteniWebApp.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PojisteniWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ApplicationDbContext _context;

        // Dependency Injection          
        // UserManager<T> - Nástroj pro operace s uživateli (vytváření, mazání, hledání, správa hesel atd.).
        // SignInManager<T> - Nástroj pro proces přihlášení a odhlášení (práce s cookies, ověřování hesel atd.).
        public AccountController
        (
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context
        )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }

        // --- PŘIHLÁŠENÍ ---

        // Tato metoda se zavolá, když uživatel přejde na /Account/Login (metodou GET).
        // Jejím úkolem je pouze zobrazit prázdný přihlašovací formulář.
        public IActionResult Login(string? returnUrl = null)
        {
            // Do ViewData si uložíme případnou URL, na kterou se má uživatel vrátit po přihlášení.
            ViewData["ReturnUrl"] = returnUrl;
            return View(); // Vrátí pohled Login.cshtml
        }

        // TATO METODA SE ZAVOLÁ PO ODESLÁNÍ PŘIHL. FORMULÁŘE (PROTO MUSÍ MÍT [HttpPost])
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // Po přihlášení admina přesměrujeme na seznam klientů, usera na jeho profil
                    if (await userManager.IsInRoleAsync(await userManager.FindByEmailAsync(model.Email), UserRoles.Admin))
                    {
                        return RedirectToAction("Index", "Clients");
                    }
                    return RedirectToAction("Index", "Profile");
                }
                ModelState.AddModelError("Login error", "Neplatné přihlašovací údaje.");
            }
            return View(model);
        }

        // --- REGISTRACE ---

        // Tato metoda se zavolá, když uživatel přejde na /Account/Register (metodou GET).
        // Zobrazí prázdný registrační formulář.
        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(); // Vrátí pohled Register.cshtml
        }

        // Tato metoda se zavolá po odeslání registračního formuláře (metodou POST).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // 1. Vytvoříme uživatelský účet (pro přihlášení)
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // 2. Vytvoříme záznam klienta (pro údaje o osobě) a uložíme ho
                    var client = new Client
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        Street = model.Street,
                        City = model.City,
                        Zip = model.Zip
                    };
                    _context.Add(client);

                    // 3. Přiřadíme roli "user"
                    await userManager.AddToRoleAsync(user, UserRoles.User);

                    await _context.SaveChangesAsync(); // Uložíme klienta do databáze

                    // 4. Přihlásíme nového uživatele
                    await signInManager.SignInAsync(user, isPersistent: false);

                    // Po úspěšné registraci přesměrujeme uživatele rovnou na jeho profil
                    return RedirectToAction("Index", "Profile");
                }

                // Pokud registrace selhala, zobrazíme chyby
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // --- ODHLÁŠENÍ (zůstává beze změny) ---
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // --- POMOCNÁ METODA ---
        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}