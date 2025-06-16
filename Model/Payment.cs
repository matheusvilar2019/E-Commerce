namespace E_Commerce.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
    }
}
