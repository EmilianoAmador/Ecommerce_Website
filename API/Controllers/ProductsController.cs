using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]             //API controller attribute
    [Route("api/[controller]")]  //Routing attribute
    public class ProductsController : ControllerBase  //public so that the methods insiode can be accessed outside this particular class. 
    { //end points go here ie a couple of methods
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>>  GetProducts()
        {
            var products = await _context.Products.ToListAsync();    // creates query in our db and stores them in the variable for an intances of the call

            return Ok(products);
        }

        [HttpGet("{id}")]                                         // will be used to get a single product
        public async Task<ActionResult<Product>>  GetProducts(int id)
        {
            return await _context.Products.FindAsync(id);
        }

    }
}