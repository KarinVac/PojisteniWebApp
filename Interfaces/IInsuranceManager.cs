using PojisteniWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PojisteniWebApp.Interfaces
{    
    public interface IInsuranceManager
    {
        // Získá seznam všech pojištění
        Task<List<InsuranceViewModel>> GetAllInsurances();

        // Najde jedno konkrétní pojištění podle jeho ID
        Task<InsuranceViewModel?> FindInsuranceById(int id);

        // Přidá nové pojištění do databáze
        Task<InsuranceViewModel> AddInsurance(InsuranceViewModel insuranceViewModel);

        // Upraví existující pojištění
        Task<InsuranceViewModel?> UpdateInsurance(InsuranceViewModel insuranceViewModel);

        // Smaže pojištění podle jeho ID
        Task RemoveInsuranceWithId(int id);
    }
}

