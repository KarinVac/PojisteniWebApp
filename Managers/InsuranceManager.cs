using AutoMapper;
using PojisteniWebApp.Interfaces;
using PojisteniWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PojisteniWebApp.Managers
{
    public class InsuranceManager(IInsuranceRepository insuranceRepository, IMapper mapper) : IInsuranceManager
    {
        private readonly IInsuranceRepository insuranceRepository = insuranceRepository;
        private readonly IMapper mapper = mapper;

        public async Task<List<InsuranceViewModel>> GetAllInsurances()
        {
            var insurances = await insuranceRepository.GetAllWithClientsAsync();
            return mapper.Map<List<InsuranceViewModel>>(insurances);
        }

        public async Task<InsuranceViewModel?> FindInsuranceById(int id)
        {          
            var insurance = await insuranceRepository.GetByIdWithClientAsync(id);
            return mapper.Map<InsuranceViewModel?>(insurance);
        }

        public async Task<InsuranceViewModel> AddInsurance(InsuranceViewModel insuranceViewModel)
        {
            var insurance = mapper.Map<Insurance>(insuranceViewModel);
            var addedInsurance = await insuranceRepository.Insert(insurance);
            return mapper.Map<InsuranceViewModel>(addedInsurance);
        }

        public async Task<InsuranceViewModel?> UpdateInsurance(InsuranceViewModel insuranceViewModel)
        {
            var insurance = mapper.Map<Insurance>(insuranceViewModel);
            try
            {
                var updatedInsurance = await insuranceRepository.Update(insurance);
                return mapper.Map<InsuranceViewModel>(updatedInsurance);
            }
            catch (System.InvalidOperationException)
            {
                if (!await insuranceRepository.ExistsWithId(insurance.Id))
                    return null;
                throw;
            }
        }

        public async Task RemoveInsuranceWithId(int id)
        {
            var insurance = await insuranceRepository.FindById(id);
            if (insurance is not null)
                await insuranceRepository.Delete(insurance);
        }
    }
}

