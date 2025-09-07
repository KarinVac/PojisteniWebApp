using PojisteniWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PojisteniWebApp.Interfaces
{   
    public interface IClientManager
    {
        Task<ClientViewModel?> FindClientById(int id);
        Task<List<ClientViewModel>> GetAllClients();
        Task<ClientViewModel> AddClient(ClientViewModel clientViewModel);
        Task<ClientViewModel?> UpdateClient(ClientViewModel clientViewModel);
        Task RemoveClientWithId(int id);
    }
}

