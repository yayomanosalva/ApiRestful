using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRestful.Context;
using ApiRestful.DTOs;
using ApiRestful.Entities;
using ApiRestful.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategorysController(CategoryService categoryService) {
            _categoryService = categoryService;
        }

        // GET: api/<CategorysController>
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {
            return Ok(await _categoryService.GetAll());
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
            return Ok(await _categoryService.Save(categoryDto));
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
