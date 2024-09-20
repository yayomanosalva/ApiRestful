using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ApiRestful.Context;
using ApiRestful.DTOs;
using ApiRestful.Entities;


namespace ApiRestful.Services
{
    public class CategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            var listDto = new List<CategoryDTO>();
            var listDB = await _context.Categories.ToListAsync();

            foreach (var category in listDB)
            {
                listDto.Add(new CategoryDTO
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                });
            }
            return listDto;
        }

        public async Task<List<CategoryDTO>> Save(CategoryDTO categoryDto)
        {
            try
            {
                var categoryDB = new Category
                {
                    Name = categoryDto.Name
                };

                await _context.Categories.AddAsync(categoryDB);
                await _context.SaveChangesAsync();

                var categoryDtoResult = new CategoryDTO
                {
                    Name = categoryDB.Name
                };

                return new List<CategoryDTO> { categoryDtoResult };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la categoría: " + ex.Message);

            }
        }
    }
}
