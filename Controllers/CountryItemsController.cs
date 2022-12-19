using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CountriesCapitalAPI.Data;
using CountriesCapitalAPI.Models;

namespace CountriesCapitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryItemsController : ControllerBase
    {
        private readonly CountryContext _context;

        public CountryItemsController(CountryContext context)
        {
            _context = context;
        }

        // GET: api/CountryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryItem>>> GetcountryItems()
        {
            var countryItems = await _context.countryItems.ToListAsync();
            var populationCounts = await _context.PopulationCounts.ToListAsync();
            foreach (var countryItem in countryItems)
            {
                countryItem.PopulationCounts.AddRange(populationCounts.FindAll(x => x.CountryItemId == countryItem.Id));
            }
            return countryItems;
        }

        // GET: api/CountryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryItem>> GetCountryItem(int id)
        {
            var countryItem = await _context.countryItems.FindAsync(id);
            var populationCounts = await _context.PopulationCounts.ToListAsync();
            if (countryItem == null)
            {
                return NotFound();
            }

            countryItem.PopulationCounts.AddRange(populationCounts.FindAll(x => x.CountryItemId == countryItem.Id));

            return countryItem;
        }

        // PUT: api/CountryItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryItem(int id, CountryItem countryItem)
        {
            if (id != countryItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(countryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryItemExists(id))
                {
                    await PostCountryItem(new List<CountryItem>(){ countryItem });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CountryItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CountryItem>> PostCountryItem(IEnumerable<CountryItem> countryItems)
        {
            foreach(var countryItem in countryItems)
            {
                _context.countryItems.Add(countryItem);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        private bool CountryItemExists(int id)
        {
            return _context.countryItems.Any(e => e.Id == id);
        }
    }
}
