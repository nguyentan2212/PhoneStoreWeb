using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneStoreWeb.API.Services.ProductServices;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Data.Contexts;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Communication.Products;

namespace PhoneStoreWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class ProductsController : ControllerBase
    {
        private readonly PhoneStoreDbContext _context;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        public ProductsController(PhoneStoreDbContext context, IMapper mapper, IProductService productService)
        {
            _context = context;
            this.mapper = mapper;
            this.productService = productService;
        }

        // GET: api/Products 
        [HttpGet]
        public async Task<ActionResult<ResponseResult<IEnumerable<ProductResponse>>>> GetProducts()
        {
            var result = await productService.GetAllProducts();
            var response = new ResponseResult<IEnumerable<ProductResponse>>();
            if (result == null)
            {
                return NotFound(response.Failed("Not found"));
            }
            return Ok(response.Succeed(result));
        }

        // GET: api/Products/5
        [HttpGet("{id}")]        
        public async Task<ActionResult> GetProduct(int id)
        {
            var result = await productService.GetById(id);
            var response = new ResponseResult<ProductResponse>();
         
            if (result == null)
            {
                return NotFound(response.Failed("Not found"));
            }             
            return Ok(response.Succeed(result));
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]        
        public async Task<IActionResult> PutProduct(UpdateProductRequest request)
        {
            var response = new ResponseResult<string>();
            if (!ModelState.IsValid)
            {
                var errors = productService.GetErrors(ModelState);
                return BadRequest(response.Failed("Data wrong", errors));
            }

            string result = await productService.Update(request);
            if (result != null)
            {
                return BadRequest(response.Failed(result));               
            }
            return Ok(response.Succeed("Succeed"));
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
