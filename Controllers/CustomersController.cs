using AlintaEnergyWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlintaEnergyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomersController(CustomerContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomer([FromQuery] long id)
        {
            var Customer = await _context.Customers.FindAsync(id);

            if (Customer == null)
            {
                return NotFound();
            }

            return Customer;
        }

        // GET: api/Customers/{search}
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Customer>>> Search([FromQuery] string name)
        {
            //var customers = from c in _context.Customers
            //                select c;

            IQueryable<Customer> query = _context.Customers;

            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(s => s.FirstName.Contains(name)
                                            || s.LastName.Contains(name));
            }

            return await query.ToListAsync();
        }

        // PUT: api/Customers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(long id, Customer Customer)
        {
            if (id != Customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(Customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer([FromBody] Customer Customer)
        {
            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = Customer.Id }, Customer);
        }

        // DELETE: api/Customers/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(long id)
        {
            var Customer = await _context.Customers.FindAsync(id);
            if (Customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(Customer);
            await _context.SaveChangesAsync();

            return Customer;
        }

        private bool CustomerExists(long id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
