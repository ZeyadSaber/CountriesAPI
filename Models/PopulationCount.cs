using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountriesCapitalAPI.Models
{
    public class PopulationCount
    {
        public int Id { get; set; }
        public string Year { get; set; } = string.Empty;
        public string? Value { get; set; }
        public string? Sex { get; set; }
        [MaxLength(100)]
        public string? Reliabilty { get; set; }
        public int? CountryItemId { get; set; }
    }
}
