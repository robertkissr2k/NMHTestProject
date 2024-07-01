using NMHTestProject.Dto;

namespace NMHTestProject.Services
{
    public interface ICalculationService
    {
        Task<CalculationOutput> CalculateAsync(int key, CalculationInput calculationInput);
    }
}