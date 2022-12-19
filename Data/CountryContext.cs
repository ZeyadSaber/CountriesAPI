using CountriesCapitalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CountriesCapitalAPI.Data
{
    public class CountryContext : DbContext
    {
        public CountryContext(DbContextOptions<CountryContext> options) : base(options)
        {
        }

        public DbSet<CountryItem> countryItems { get; set; }
    }
}
