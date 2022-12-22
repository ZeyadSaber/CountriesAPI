using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CountriesCapitalAPI.Data;
using CountriesCapitalAPI.Models;
using NuGet.Protocol;

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
        public async Task<ActionResult<IEnumerable<CountryItem>>> GetcountryItems([FromQuery] PaginationParams @params)
        {
            var countryItems = await (from c in _context.countryItems.Skip((@params.Page - 1) * 50).Take(50)
                                      select new CountryItem
                                      {
                                          Id = c.Id,
                                          City = c.City,
                                          Country = c.Country,
                                          PopulationCounts = (from p in _context.PopulationCounts
                                                              where p.CountryItemId == c.Id
                                                              select p).ToList()
                                      }
                                      ).ToListAsync();

            return countryItems;
        }

        // GET: api/CountryItems/5
        [HttpGet("{id}")]
        public ActionResult<CountryItem> GetCountryItem(int id)
        {
            var countryItem =  (from c in _context.countryItems
                                   where c.Id == id
                                   select new CountryItem
                                   {
                                       Id = id,
                                       City = c.City,
                                       Country = c.Country,
                                       PopulationCounts = (from p in _context.PopulationCounts
                                                           where p.CountryItemId == id
                                                           select p).ToList()
                                   });
            if (countryItem == null)
            {
                return NotFound();
            }


            return (CountryItem)countryItem;
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
        public async Task<ActionResult<IEnumerable<CountryItem>>> PostCountryItem(IEnumerable<CountryItem> countryItems)
        {
            foreach(var countryItem in countryItems)
            {
                _context.countryItems.Add(countryItem);
                await _context.SaveChangesAsync();
            }

            return Ok(countryItems);
        }

        private bool CountryItemExists(int id)
        {
            return _context.countryItems.Any(e => e.Id == id);
        }
    }
}
