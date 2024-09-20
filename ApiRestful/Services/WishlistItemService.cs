using ApiRestful.Context;
using ApiRestful.DTOs;
using ApiRestful.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class WishlistItemService
{
    private readonly AppDbContext _context;
    public WishlistItemService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool IsSuccess, string Message, WishlistItemDTO WishlistItem)> Save(WishlistItemDTO wishlistItemDto)
    {
        try
        {
            var productExists = await _context.Products.AnyAsync(p => p.ProductId == wishlistItemDto.ProductId);
            if (!productExists)
            {
                return (false, $"Producto con ID {wishlistItemDto.ProductId} no encontrado.", null);
            }

            var wishlistDB = new WishlistItem
            {
                Id = wishlistItemDto.Id,
                UserId = wishlistItemDto.UserId,
                ProductId = wishlistItemDto.ProductId
            };

            await _context.WishlistItems.AddAsync(wishlistDB);
            await _context.SaveChangesAsync();

            var wishlistItemDtoResult = new WishlistItemDTO
            {
                ProductId = wishlistDB.ProductId,
                UserId = wishlistDB.UserId
            };

            return (true, "Wishlist item added successfully.", wishlistItemDtoResult);
        }
        catch (DbUpdateException ex)
        {
            var sqlException = ex.InnerException as SqlException;
            if (sqlException != null)
            {
                return (false, $"An error occurred while saving the wishlist item: {sqlException.Message}", null);
            }
            return (false, "An unexpected error occurred.", null);
        }
    }

    public async Task<(bool IsSuccess, string Message)> Delete(int id)
    {
        try
        {
            var wishlistItemDB = await _context.WishlistItems.FindAsync(id);
            if (wishlistItemDB == null)
            {
                return (false, $"Wish list Items con ID {id} no encontrado.");
            }

            _context.WishlistItems.Remove(wishlistItemDB);
            await _context.SaveChangesAsync();

            return (true, $"Wish list Items with ID {id} deleted successfully.");
        }
        catch (DbUpdateException ex)
        {
            return (false, ex.Message);
        }
    }
}
