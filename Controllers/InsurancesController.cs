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
        private readonly IClientManager clientManager = clientManager;

        public async Task<IActionResult> Index()
        {
            var insurances = await insuranceManager.GetAllInsurances();
            return View(insurances);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var insurance = await insuranceManager.FindInsuranceById(id.Value);
            return insurance == null ? NotFound() : View(insurance);
        }

        public async Task<IActionResult> Create(int? clientId)
        {
            ViewBag.ClientList = new SelectList(await clientManager.GetAllClients(), "Id", "FullName", clientId);
                      
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuranceViewModel insuranceViewModel)
        {
            if (ModelState.IsValid)
            {
                await insuranceManager.AddInsurance(insuranceViewModel);
                return RedirectToAction(nameof(Index));
            }            
            ViewBag.ClientList = new SelectList(await clientManager.GetAllClients(), "Id", "FullName", insuranceViewModel.ClientId);
            return View(insuranceViewModel);
        }
                
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var insurance = await insuranceManager.FindInsuranceById(id.Value);
            if (insurance == null) return NotFound();

            var client = await clientManager.FindClientById(insurance.ClientId);
            ViewBag.ClientName = client?.FullName;

            return View(insurance);
        }

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
            var client = await clientManager.FindClientById(insuranceViewModel.ClientId);
            ViewBag.ClientName = client?.FullName;
            return View(insuranceViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var insurance = await insuranceManager.FindInsuranceById(id.Value);
            return insurance == null ? NotFound() : View(insurance);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await insuranceManager.RemoveInsuranceWithId(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

