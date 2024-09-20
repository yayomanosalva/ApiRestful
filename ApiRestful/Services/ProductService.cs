using ApiRestful.Context;
using ApiRestful.DTOs;
using ApiRestful.Entities;
using Microsoft.Data.SqlClient;

using Microsoft.EntityFrameworkCore;
namespace ApiRestful.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDTO>> GetAll()
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
            return listDto;
        }

        public async Task<(bool IsSuccess, string Message, ProductDTO? Product)> GetOne(int sendId)
        {
            try
            {
                var productDB = await _context.Products.Include(p => p.Category)
                                      .Where(e => e.ProductId == sendId)
                                      .FirstOrDefaultAsync();
                if (productDB == null)
                {
                    return (false, $"Producto con ID {sendId} no encontrado.", null);
                }

                var productDto = new ProductDTO
                {
                    ProductId = productDB.ProductId,
                    Name = productDB.Name,
                    Description = productDB.Description,
                    Price = productDB.Price
                };

                return (true, $"Producto con ID {sendId} encontrado exitosamente.", productDto);
            }
            catch (DbUpdateException ex)
            {
                return (false, ex.Message, null);
            }
        }

        public async Task<(bool IsSuccess, string Message, ProductDTO productDto)> Save(ProductDTO productDto)
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

                var productDtoResult = new ProductDTO
                {
                    Name = productDB.Name,
                    Description = productDB.Description,
                    Price = productDB.Price,
                    CategoryId = productDB.CategoryId,
                };

                return (true, "Product added successfully.", productDtoResult);
            }
            catch (Exception ex)
            {
                var sqlException = ex.InnerException as SqlException;
                if (sqlException != null)
                {
                    return (false, $"An error occurred while saving the product item: {sqlException.Message}", null);
                }
                return (false, "An unexpected error occurred.", null);
            }
        }
    }
}
