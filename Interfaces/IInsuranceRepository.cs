using PojisteniWebApp.Models;

namespace PojisteniWebApp.Interfaces
{
    public interface IInsuranceRepository : IBaseRepository<Insurance>
    {
        Task<List<Insurance>> GetAllWithClientsAsync();
        Task<Insurance?> GetByIdWithClientAsync(int id);
    }
}

