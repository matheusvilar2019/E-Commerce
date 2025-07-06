namespace E_Commerce.Model
{
    public class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ZipCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string AddressLine2 { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}
