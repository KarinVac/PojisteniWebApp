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

        public async Task<IActionResult> Index()
        {
            var clients = await clientManager.GetAllClients();
            return View(clients);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var client = await clientManager.FindClientById(id.Value);
            return client == null ? NotFound() : View(client);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                var newClient = await clientManager.AddClient(clientViewModel);
                
                return RedirectToAction("CreateSuccess", new { newClientId = newClient.Id, newClientName = newClient.FullName });
            }
            return View(clientViewModel);
        }

        public IActionResult CreateSuccess(int newClientId, string newClientName)
        {
            ViewBag.NewClientId = newClientId;
            ViewBag.NewClientName = newClientName;
            return View();
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var client = await clientManager.FindClientById(id.Value);
            return client == null ? NotFound() : View(client);
        }

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
                    ViewBag.ErrorMessage = "Došlo k chybě, pojištěnec nebyl nalezen.";
                }
                else
                {
                    ViewBag.SuccessMessage = "Změny byly úspěšně uloženy.";
                }
            }            
            return View(clientViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var client = await clientManager.FindClientById(id.Value);
            return client == null ? NotFound() : View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await clientManager.RemoveClientWithId(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

