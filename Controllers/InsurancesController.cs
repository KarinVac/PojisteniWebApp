using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PojisteniWebApp.Interfaces;
using PojisteniWebApp.Models;
using System.Threading.Tasks;

namespace PojisteniWebApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class InsurancesController(IInsuranceManager insuranceManager, IClientManager clientManager) : Controller
    {
        private readonly IInsuranceManager insuranceManager = insuranceManager;
        private readonly IClientManager clientManager = clientManager; // pro seznam klientů

        // Zobrazí seznam všech pojištění
        public async Task<IActionResult> Index()
        {
            var insurances = await insuranceManager.GetAllInsurances();
            return View(insurances);
        }

        // Zobrazí detail jednoho pojištění
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var insurance = await insuranceManager.FindInsuranceById(id.Value);
            return insurance == null ? NotFound() : View(insurance);
        }

        // Zobrazí formulář pro vytvoření nového pojištění
        public async Task<IActionResult> Create(int? clientId)
        {
            // Připraví seznam všech klientů pro dropdown
            ViewBag.ClientList = new SelectList(await clientManager.GetAllClients(), "Id", "FullName", clientId);

            // Pokud přicházíme s předvybraným klientem
            if (clientId.HasValue)
            {
                var client = await clientManager.FindClientById(clientId.Value);
                if (client != null)
                {
                    ViewBag.ClientName = client.FullName;
                    ViewBag.ClientId = client.Id;
                }
            }
            return View();
        }

        // Zpracuje odeslaný formulář pro vytvoření pojištění
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuranceViewModel insuranceViewModel)
        {
            if (ModelState.IsValid)
            {
                await insuranceManager.AddInsurance(insuranceViewModel);
                return RedirectToAction(nameof(Index));
            }
            // Pokud formulář není validní, znovu načteme seznam klientů
            ViewBag.ClientList = new SelectList(await clientManager.GetAllClients(), "Id", "FullName", insuranceViewModel.ClientId);
            return View(insuranceViewModel);
        }

        // Zobrazí formulář pro úpravu pojištění
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var insurance = await insuranceManager.FindInsuranceById(id.Value);
            if (insurance == null) return NotFound();

            // Pro zobrazení jména klienta načteme i jeho data
            var client = await clientManager.FindClientById(insurance.ClientId);
            ViewBag.ClientName = client?.FullName;

            return View(insurance);
        }

        // Zpracuje odeslaný formulář pro úpravu pojištění
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InsuranceViewModel insuranceViewModel)
        {
            if (id != insuranceViewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var updatedInsurance = await insuranceManager.UpdateInsurance(insuranceViewModel);
                if (updatedInsurance == null)
                {
                    ViewBag.ErrorMessage = "Došlo k chybě, pojištění nebylo nalezeno.";
                }
                else
                {
                    ViewBag.SuccessMessage = "Změny byly úspěšně uloženy.";
                }
            }
            // Znovu načteme jméno klienta, aby se zobrazilo ve formuláři
            var client = await clientManager.FindClientById(insuranceViewModel.ClientId);
            ViewBag.ClientName = client?.FullName;
            return View(insuranceViewModel);
        }

        // Zobrazí stránku pro potvrzení smazání
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var insurance = await insuranceManager.FindInsuranceById(id.Value);
            return insurance == null ? NotFound() : View(insurance);
        }

        // Provede samotné smazání pojištění
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await insuranceManager.RemoveInsuranceWithId(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

