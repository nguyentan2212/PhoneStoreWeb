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
using Newtonsoft.Json;

namespace PhoneStoreWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {          
            this.productService = productService;
        }

        // GET: api/Products 
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await productService.GetAllProducts();
            var response = new ResponseResult<List<ProductResponse>>();             
            return Ok(response.Succeed(result));
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [HttpPost]
        public async Task<IActionResult> PostProduct(CreateProductRequest request)
        {
            var response = new ResponseResult<string>();
            if (!ModelState.IsValid)
            {
                var errors = productService.GetErrors(ModelState);
                return BadRequest(response.Failed("Data wrong", errors));
            }
            string result = await productService.Create(request);
            if (result != null)
            {
                return BadRequest(response.Failed(result));
            }
            return Ok(response.Succeed("Succeed"));
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = new ResponseResult<string>();
            string result = await productService.Delete(id);
            if (result != null)
            {
                return BadRequest(response.Failed(result));
            }
            return Ok(response.Succeed("Succeed"));
        }

        // GET:api/products/category/3
        [HttpGet("category/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductByCategory(int id)
        {
            var result = await productService.GetAllProductsByCategory(id);
            var response = new ResponseResult<IEnumerable<ProductResponse>>();
            if (result == null)
            {
                return NotFound(response.Failed("Not found"));
            }
            return Ok(response.Succeed(result));
        }

        // POST: api/products/image/
        [HttpPost("image")]
        public async Task<IActionResult> PostImage(AddProductImageRequest request)
        {
            var response = new ResponseResult<string>();
            if (!ModelState.IsValid)
            {
                var errors = productService.GetErrors(ModelState);
                return BadRequest(response.Failed("Data wrong", errors));
            }
            string result = await productService.AddImage(request);
            if (result != null)
            {
                return BadRequest(response.Failed(result));
            }
            return Ok(response.Succeed("Succeed"));
        }
    }
}
