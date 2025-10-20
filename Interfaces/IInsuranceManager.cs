using PojisteniWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PojisteniWebApp.Interfaces
{    
    public interface IInsuranceManager
    {
        Task<List<InsuranceViewModel>> GetAllInsurances();

        Task<InsuranceViewModel?> FindInsuranceById(int id);

        Task<InsuranceViewModel> AddInsurance(InsuranceViewModel insuranceViewModel);

        Task<InsuranceViewModel?> UpdateInsurance(InsuranceViewModel insuranceViewModel);

        Task RemoveInsuranceWithId(int id);
    }
}

