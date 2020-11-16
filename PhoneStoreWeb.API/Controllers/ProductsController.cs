﻿using System;
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

namespace PhoneStoreWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return Ok(response.Succeed(result));
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var result = await productService.GetAllProducts();
            var response = new ResponseResult<IEnumerable<ProductResponse>>();

            if (result == null)
            {
                return NotFound(response.Failed("Not found"));
            }
            return Ok(response.Succeed(result));
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
