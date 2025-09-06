using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PojisteniWebApp.Data;

namespace PojisteniWebApp.Controllers;

[Authorize(Roles = UserRoles.User)] // Přístup na tuto stránku má jen uživatel s rolí "user"
public class ProfileController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public ProfileController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // Metoda pro zobrazení osobní stránky uživatele
    public async Task<IActionResult> Index()
    {
        // Zjistíme email přihlášeného uživatele
        var userEmail = User.Identity.Name;

        // Podle emailu najdeme v naší databázi klientů odpovídající záznam
        // a rovnou načteme i jeho pojištění.
        var client = await _context.Client
                                   .Include(c => c.Insurances)
                                   .FirstOrDefaultAsync(c => c.Email == userEmail);

        // Pokud pro daný email neexistuje záznam klienta, zobrazíme chybovou stránku
        if (client == null)
        {
            return View("NotFound"); // Musíme vytvořit jednoduchý pohled NotFound.cshtml
        }

        // Předáme data klienta do pohledu
        return View(client);
    }
}
