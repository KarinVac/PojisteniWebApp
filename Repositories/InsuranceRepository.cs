using PojisteniWebApp.Data;
using PojisteniWebApp.Interfaces;
using PojisteniWebApp.Models;

namespace PojisteniWebApp.Repositories
{  
    public class InsuranceRepository : BaseRepository<Insurance>, IInsuranceRepository
    {
        public InsuranceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

