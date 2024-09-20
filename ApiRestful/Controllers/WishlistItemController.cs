using ApiRestful.Context;
using ApiRestful.DTOs;
using ApiRestful.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiRestful.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WishlistItemController : ControllerBase
    {

        private readonly AppDbContext _context;
        public WishlistItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("save")]
        public async Task<ActionResult<WishlistItemDTO>> Save(WishlistItemDTO wishlistItemDto)
        {
            try
            {
                // Verificar si el ProductId existe en la tabla Products
                var productExists = await _context.Products.AnyAsync(p => p.ProductId == wishlistItemDto.ProductId);
                if (!productExists)
                {
                    return NotFound(new { message = $"Producto con ID {wishlistItemDto.ProductId} no encontrado." });
                }

                var wishlistDB = new WishlistItem
                {
                    Id = wishlistItemDto.Id,
                    UserId = wishlistItemDto.UserId,
                    ProductId = wishlistItemDto.ProductId
                };

                await _context.WishlistItems.AddAsync(wishlistDB);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Wishlist item added successfully.", wishlistItem = wishlistDB });
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;

                if (sqlException != null)
                {
                    Console.WriteLine(sqlException.Message);
                    return StatusCode(500, new { message = "An error occurred while saving the wishlist item.", details = sqlException.Message });
                }
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<WishlistItemDTO>> Delete(int id)
        {
            try
            {
                var WishlistItemDB = await _context.WishlistItems.FindAsync(id);
                if (WishlistItemDB == null)
                {
                    return StatusCode(500, new { message = $"Wish list Items con ID {id} no encontrado." });
                }

                return Ok(new { message = $"Wish list Items with ID {id} deleted successfully." });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
