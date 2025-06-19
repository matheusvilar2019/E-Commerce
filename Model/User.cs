namespace E_Commerce.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime BirthDate { get; set; }
        public int CEP { get; set; }
        public string Address { get; set; }
        public Cart Cart { get; set; }
        public IList<Role> Roles { get; set; }
        public List<Payment> Payments { get; set; }

    }
}
