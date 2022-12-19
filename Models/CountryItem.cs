using CountriesCapitalAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CountriesCapitalAPI.Models
{
    public class CountryItem
    {
        public int Id { get; set; }
        public string City { get; set; }

        public string Country { get; set; }
        public List<PopulationCount> PopulationCounts { get; set; }
    }
}