namespace E_Commerce.ViewModels.Carts
{
    public class GetCartDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Closed { get; set; }

        public List<GetCartItemDTO> Items { get; set; } = new List<GetCartItemDTO>();
    }
}
