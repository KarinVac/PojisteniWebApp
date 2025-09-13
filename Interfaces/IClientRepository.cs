using PojisteniWebApp.Models;

namespace PojisteniWebApp.Interfaces
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Task<Client?> GetByIdWithInsurancesAsync(int id);
    }
}