using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using ApiRestful.Context;
using ApiRestful.DTOs;
using ApiRestful.Entities;
using System.Linq;

namespace ApiRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<ProductDTO>>> Get()
        {
            var listDto = new List<ProductDTO>();
            var listDB = await _context.Products.ToListAsync();

            foreach (var product in listDB)
            {
                listDto.Add(new ProductDTO
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                });
            }
            return Ok(listDto);
        }

        [HttpGet]
        [Route("detail/{sendId}")]
        public async Task<ActionResult<ProductDTO>> Get(int sendId)
        {
            var productDto = new ProductDTO();
            var productDB = await _context.Products.Include(p => p.Category)
                                  .Where(e => e.ProductId == sendId)
                                  .FirstOrDefaultAsync();

            if (productDB == null)
            {
                return NotFound(new { message = $"Producto con ID {sendId} no encontrado." });
            }

            productDto.ProductId = productDB.ProductId;
            productDto.Name = productDB.Name;
            productDto.Description = productDB.Description;
            productDto.Price = productDB.Price;

            return Ok(new { message = "product added successfully.", product = productDto });
        }

        [HttpPost]
        [Route("save")]

        public async Task<ActionResult<ProductDTO>> Save(ProductDTO productDto)
        {
            try
            {
                var productDB = new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    CategoryId = productDto.CategoryId,
                };

                await _context.Products.AddAsync(productDB);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Product added successfully.", product = productDB });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
