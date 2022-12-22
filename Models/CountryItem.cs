using CountriesCapitalAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CountriesCapitalAPI.Models
{
    public class CountryItem
    {
        public int Id { get; set; }
        [MaxLength (100)]
        public string City { get; set; } = String.Empty;
        [MaxLength (100)]
        public string Country { get; set; } = String.Empty ;
        public List<PopulationCount>? PopulationCounts { get; set; }
    }
}