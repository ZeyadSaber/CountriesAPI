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
            return await _context.countryItems.ToListAsync();
        }

        // GET: api/CountryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryItem>> GetCountryItem(int id)
        {
            var countryItem = await _context.countryItems.FindAsync(id);

            if (countryItem == null)
            {
                return NotFound();
            }

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
                    return NotFound();
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
        public async Task<ActionResult<CountryItem>> PostCountryItem(CountryItem countryItem)
        {
            _context.countryItems.Add(countryItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountryItem", new { id = countryItem.Id }, countryItem);
        }

        // DELETE: api/CountryItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryItem(int id)
        {
            var countryItem = await _context.countryItems.FindAsync(id);
            if (countryItem == null)
            {
                return NotFound();
            }

            _context.countryItems.Remove(countryItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryItemExists(int id)
        {
            return _context.countryItems.Any(e => e.Id == id);
        }
    }
}
