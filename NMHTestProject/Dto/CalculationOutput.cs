using System.Text.Json.Serialization;

namespace NMHTestProject.Dto
{
    public class CalculationOutput
    {
        [JsonPropertyName("computed_value")]
        public decimal ComputedValue { get; set; }

        [JsonPropertyName("input_value")]
        public decimal InputValue { get; set; }

        [JsonPropertyName("previous_value")]
        public double PreviousValue { get; set; }
    }
}
