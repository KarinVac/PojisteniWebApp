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
            return await dbSet.Include(c => c.Insurances).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}

