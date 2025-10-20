using AutoMapper;
using PojisteniWebApp.Interfaces;
using PojisteniWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PojisteniWebApp.Managers
{
    public class ClientManager(IClientRepository clientRepository, IMapper mapper) : IClientManager
    {
        private readonly IClientRepository clientRepository = clientRepository;
        private readonly IMapper mapper = mapper;

        public async Task<ClientViewModel?> FindClientById(int id)
        {            
            Client? client = await clientRepository.GetByIdWithInsurancesAsync(id);
            return mapper.Map<ClientViewModel?>(client);
        }

        public async Task<List<ClientViewModel>> GetAllClients()
        {
            List<Client> clients = await clientRepository.GetAll();
            return mapper.Map<List<ClientViewModel>>(clients);
        }

        public async Task<ClientViewModel> AddClient(ClientViewModel clientViewModel)
        {
            Client client = mapper.Map<Client>(clientViewModel);
            Client addedClient = await clientRepository.Insert(client);
            return mapper.Map<ClientViewModel>(addedClient);
        }

        public async Task<ClientViewModel?> UpdateClient(ClientViewModel clientViewModel)
        {
            Client client = mapper.Map<Client>(clientViewModel);
            try
            {
                Client updatedClient = await clientRepository.Update(client);
                return mapper.Map<ClientViewModel>(updatedClient);
            }
            catch (System.InvalidOperationException)
            {
                if (!await clientRepository.ExistsWithId(client.Id))
                    return null;
                throw;
            }
        }

        public async Task RemoveClientWithId(int id)
        {
            Client? client = await clientRepository.FindById(id);
            if (client is not null)
                await clientRepository.Delete(client);
        }
    }
}

