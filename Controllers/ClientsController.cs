using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PojisteniWebApp.Interfaces;
using PojisteniWebApp.Models;
using System.Threading.Tasks;

namespace PojisteniWebApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ClientsController(IClientManager clientManager) : Controller
    {
        private readonly IClientManager clientManager = clientManager;

        // Zobrazí seznam všech klientů
        public async Task<IActionResult> Index()
        {
            var clients = await clientManager.GetAllClients();
            return View(clients);
        }

        // Zobrazí detail jednoho klienta
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var client = await clientManager.FindClientById(id.Value);
            return client == null ? NotFound() : View(client);
        }

        // Zobrazí formulář pro vytvoření nového klienta
        public IActionResult Create()
        {
            return View();
        }

        // Zpracuje odeslaný formulář pro vytvoření klienta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                var newClient = await clientManager.AddClient(clientViewModel);
                // Po vytvoření přesměrujeme na stránku s potvrzením
                return RedirectToAction("CreateSuccess", new { newClientId = newClient.Id, newClientName = newClient.FullName });
            }
            return View(clientViewModel);
        }

        // Zobrazí stránku s potvrzením o vytvoření
        public IActionResult CreateSuccess(int newClientId, string newClientName)
        {
            ViewBag.NewClientId = newClientId;
            ViewBag.NewClientName = newClientName;
            return View();
        }

        // Zobrazí formulář pro úpravu klienta
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var client = await clientManager.FindClientById(id.Value);
            return client == null ? NotFound() : View(client);
        }

        // Zpracuje odeslaný formulář pro úpravu klienta 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClientViewModel clientViewModel)
        {
            if (id != clientViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updatedClient = await clientManager.UpdateClient(clientViewModel);
                if (updatedClient == null)
                {
                    // Tento případ nastane, pokud byl klient mezitím smazán jiným uživatelem
                    ViewBag.ErrorMessage = "Došlo k chybě, pojištěnec nebyl nalezen.";
                }
                else
                {
                    // Úspěšné uložení
                    ViewBag.SuccessMessage = "Změny byly úspěšně uloženy.";
                }
            }            
            return View(clientViewModel);
        }

        // Zobrazí stránku pro potvrzení smazání
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var client = await clientManager.FindClientById(id.Value);
            return client == null ? NotFound() : View(client);
        }

        // Provede samotné smazání klienta
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await clientManager.RemoveClientWithId(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

