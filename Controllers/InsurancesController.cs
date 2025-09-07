using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PojisteniWebApp.Data;
using PojisteniWebApp.Models;
using System.Threading.Tasks;

namespace PojisteniWebApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class InsurancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsurancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Insurances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Insurance.Include(i => i.Client);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Insurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .Include(i => i.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // GET: Insurances/Create
        public IActionResult Create(int? clientId)
        {
            if (clientId.HasValue)
            {
                var client = _context.Client.Find(clientId.Value);
                if (client != null)
                {
                    ViewBag.ClientName = client.FullName;
                    ViewBag.ClientId = client.Id;
                }
            }
            else
            {
                ViewBag.ClientList = new SelectList(_context.Client, "Id", "FullName");
            }

            return View();
        }

        // POST: Insurances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Amount,StartDate,EndDate,ClientId")] Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }                   
            
            ViewBag.ClientList = new SelectList(_context.Client, "Id", "FullName", insurance.ClientId);
            return View(insurance);
        }

        // GET: Insurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                                          .Include(i => i.Client)
                                          .FirstOrDefaultAsync(i => i.Id == id);

            if (insurance == null)
            {
                return NotFound();
            }

            ViewBag.ClientName = insurance.Client?.FullName;
            return View(insurance);
        }

        // POST: Insurances/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Amount,StartDate,EndDate,ClientId")] Insurance insurance)
        {
            if (id != insurance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insurance);
                    await _context.SaveChangesAsync();

                    ViewBag.SuccessMessage = "Změny byly úspěšně uloženy.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceExists(insurance.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Došlo k chybě při ukládání, zkuste to prosím znovu.";
                    }
                }                
            }

            // Znovu načteme jméno klienta, aby se zobrazilo ve formuláři
            var client = await _context.Client.FindAsync(insurance.ClientId);
            ViewBag.ClientName = client?.FullName;

            return View(insurance);
        }

        // GET: Insurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .Include(i => i.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // POST: Insurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insurance = await _context.Insurance.FindAsync(id);
            if (insurance != null)
            {
                _context.Insurance.Remove(insurance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceExists(int id)
        {
            return _context.Insurance.Any(e => e.Id == id);
        }
    }
}
