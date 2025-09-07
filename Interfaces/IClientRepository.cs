using PojisteniWebApp.Models;

namespace PojisteniWebApp.Interfaces
{
    // Dědí všechny metody z IBaseRepository a říká, že bude pracovat s typem Client.
    public interface IClientRepository : IBaseRepository<Client>
    {
        
    }
}