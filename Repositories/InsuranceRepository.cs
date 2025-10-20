using Microsoft.EntityFrameworkCore;
using PojisteniWebApp.Data;
using PojisteniWebApp.Interfaces;
using PojisteniWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PojisteniWebApp.Repositories
{
    public class InsuranceRepository : BaseRepository<Insurance>, IInsuranceRepository
    {
        public InsuranceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Insurance>> GetAllWithClientsAsync()
        {           
            return await dbSet.Include(i => i.Client).ToListAsync();
        }

        public async Task<Insurance?> GetByIdWithClientAsync(int id)
        {
            return await dbSet.Include(i => i.Client).FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}

