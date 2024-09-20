using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using ApiRestful.Context;
using ApiRestful.DTOs;
using ApiRestful.Entities;
using ApiRestful.Services;
using System.Linq;

namespace ApiRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<ProductDTO>>> Get()
        {
            return Ok( await _productService.GetAll());
        }

        [HttpGet]
        [Route("detail/{sendId}")]
        public async Task<ActionResult<ProductDTO>> Get(int sendId)
        {
            var result = await _productService.GetOne(sendId);
            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Message });
            }
            return Ok(new { message = result.Message, result.Product });
        }

        [HttpPost]
        [Route("save")]

        public async Task<ActionResult<ProductDTO>> Save(ProductDTO productDto)
        {
            var result = await _productService.Save(productDto);
            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Message });
            }
            return Ok(new { message = result.Message, wishlistItem = result.productDto });
        }
    }
}
