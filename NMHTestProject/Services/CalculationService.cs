using Microsoft.Extensions.Options;
using NMHTestProject.Common;
using NMHTestProject.Dto;
using System.ComponentModel.DataAnnotations;

namespace NMHTestProject.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly IGlobalKeyStorageService _globalKeyStorageService;
        private readonly IOptions<Configuration> _configurationOptions;
        private readonly ICalculationMessagingService _calculationMessagingService;
        private static readonly SemaphoreSlim _semaphore = new(1);

        public CalculationService(
            IGlobalKeyStorageService globalKeyStorageService,
            IOptions<Configuration> configurationOptions,
            ICalculationMessagingService calculationMessagingService)
        {
            _globalKeyStorageService = globalKeyStorageService;
            _configurationOptions = configurationOptions;
            _calculationMessagingService = calculationMessagingService;
        }

        public async Task<CalculationOutput> CalculateAsync(int key, CalculationInput calculationInput)
        {
            if (calculationInput.Input <= 0)
            {
                throw new ValidationException("Calculation input value has to be positive nonzero decimal number.");
            }

            var config = _configurationOptions.Value;
            await _semaphore.WaitAsync();

            try
            {
                double? previousValue = _globalKeyStorageService.Get<double?>(key.ToString());

                if (previousValue is null)
                {
                    _globalKeyStorageService.Set(key.ToString(), config.DefaultKeyStorageValue);
                    previousValue = config.DefaultKeyStorageValue;
                }

                var computedValue = Math.Log(Convert.ToDouble(calculationInput.Input));
                computedValue = computedValue / previousValue.Value;
                computedValue = Math.Pow(computedValue, 1d / 3d);

                _globalKeyStorageService.Set(key.ToString(), computedValue);

                var output = new CalculationOutput
                {
                    PreviousValue = previousValue.Value,
                    ComputedValue = Convert.ToDecimal(computedValue),
                    InputValue = calculationInput.Input
                };

                _calculationMessagingService.QueueCalculation(output);

                return output;
            }
            catch
            {
                throw;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
