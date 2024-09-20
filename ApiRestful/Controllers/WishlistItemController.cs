using ApiRestful.Context;
using ApiRestful.DTOs;
using ApiRestful.Entities;
using ApiRestful.Services;

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
        private readonly WishlistItemService _wishlistItemService;
        public WishlistItemController(WishlistItemService wishlistItemService)
        {
            _wishlistItemService = wishlistItemService;
        }

        [HttpPost]
        [Route("save")]
        public async Task<ActionResult<WishlistItemDTO>> Save(WishlistItemDTO wishlistItemDto)
        {
            var result = await _wishlistItemService.Save(wishlistItemDto);
            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Message });
            }
            return Ok(new { message = result.Message, wishlistItem = result.WishlistItem });
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<WishlistItemDTO>> Delete(int id)
        {
            var result = await _wishlistItemService.Delete(id);
            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Message });
            }
            return Ok(new { message = result.Message });
        }
    }
}
