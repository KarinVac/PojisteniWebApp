using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PojisteniWebApp.Data;

namespace PojisteniWebApp.Controllers;

[Authorize(Roles = UserRoles.User)] 
public class ProfileController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public ProfileController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userEmail = User.Identity.Name;

        var client = await _context.Client
                                   .Include(c => c.Insurances)
                                   .FirstOrDefaultAsync(c => c.Email == userEmail);

        if (client == null)
        {
            return View("NotFound");
        }

        return View(client);
    }
}
