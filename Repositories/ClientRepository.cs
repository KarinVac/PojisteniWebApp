using PojisteniWebApp.Data;
using PojisteniWebApp.Interfaces;
using PojisteniWebApp.Models;

namespace PojisteniWebApp.Repositories
{
    // Dědí od obecné šablony BaseRepository a implementuje předpis IClientRepository.
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {        
        public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}