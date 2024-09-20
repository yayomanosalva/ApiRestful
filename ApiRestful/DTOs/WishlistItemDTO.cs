using ApiRestful.Entities;

namespace ApiRestful.DTOs
{
    public class WishlistItemDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
