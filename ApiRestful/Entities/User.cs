namespace ApiRestful.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public ICollection<WishlistItem> WishlistItems { get; set; }
    }
}
