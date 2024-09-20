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
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            var listDto = new List<UserDTO>();
            var listDB = await _context.Users.ToListAsync();

            foreach (var user in listDB)
            {
                listDto.Add(new UserDTO
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email,
                });
            }
            return Ok(listDto);
        }

        [HttpPost]
        [Route("save")]

        public async Task<ActionResult<UserDTO>> Save(UserDTO userDto)
        {
            try
            {
                var userDB = new User
                {
                    UserId = userDto.UserId,
                    Username = userDto.Username,
                    Email = userDto.Email,
                };

                await _context.Users.AddAsync(userDB);
                await _context.SaveChangesAsync();
                return Ok(new { message = "User added successfully.", User = userDB });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
