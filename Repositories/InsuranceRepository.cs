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

        // TOTO JE TEN CHYBĚJÍCÍ KÓD - PLNÍME SLIBY ZE SMLOUVY

        // Metoda, která načte všechna pojištění a ke každému rovnou připojí i data o jeho klientovi.
        public async Task<List<Insurance>> GetAllWithClientsAsync()
        {
            // Použijeme .Include(), abychom řekli: "Přines všechna pojištění i s jejich klienty."
            return await dbSet.Include(i => i.Client).ToListAsync();
        }

        // Metoda, která načte jedno pojištění a rovnou k němu připojí i data o jeho klientovi.
        public async Task<Insurance?> GetByIdWithClientAsync(int id)
        {
            // Použijeme .Include(), abychom řekli: "Přines jedno pojištění i s jeho klientem."
            return await dbSet.Include(i => i.Client).FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}

