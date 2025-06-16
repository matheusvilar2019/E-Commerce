namespace E_Commerce.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
