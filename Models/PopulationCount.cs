using System.ComponentModel.DataAnnotations.Schema;

namespace CountriesCapitalAPI.Models
{
    public class PopulationCount
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public string Value { get; set; }
        public string Sex { get; set; }
        public string Reliabilty { get; set; }
        public int? CountryItemId { get; set; }
    }
}
