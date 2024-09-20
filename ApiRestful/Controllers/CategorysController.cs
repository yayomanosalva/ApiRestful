using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRestful.Context;
using ApiRestful.DTOs;
using ApiRestful.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategorysController(AppDbContext context) {
            _context = context;
        }

        // GET: api/<CategorysController>
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
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
            return Ok(listDto);
        }

        // GET api/<CategorysController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategorysController>
        [HttpPost]
        [Route("save")]

        public async Task<ActionResult<CategoryDTO>> Save(CategoryDTO categoryDto)
        {
            var categoryDB = new Category
            {
                Name = categoryDto.Name
            };

            await _context.Categories.AddAsync(categoryDB);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Product added successfully.", category = categoryDB });

        }

        // PUT api/<CategorysController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategorysController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
