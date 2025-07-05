using System.Text.Json.Serialization;

namespace E_Commerce.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public bool Closed { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
