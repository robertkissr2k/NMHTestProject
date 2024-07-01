using System.ComponentModel.DataAnnotations;

namespace NMHTestProject.Dto
{
    public class CalculationInput
    {
        [Required]
        public decimal Input { get; set; }
    }
}
