using Microsoft.EntityFrameworkCore;
using PojisteniWebApp.Data;
using PojisteniWebApp.Interfaces;
using PojisteniWebApp.Models;
using System.Threading.Tasks;

namespace PojisteniWebApp.Repositories
{
    public class ClientRepository(ApplicationDbContext dbContext) : BaseRepository<Client>(dbContext), IClientRepository
    {        
        public async Task<Client?> GetByIdWithInsurancesAsync(int id)
        {
            // Použijeme .Include(), abychom řekli Entity Frameworku,
            // aby při načítání klienta rovnou "přibalil" i všechny jeho záznamy z tabulky Insurances.
            return await dbSet.Include(c => c.Insurances).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}

