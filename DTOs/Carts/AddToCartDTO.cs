namespace E_Commerce.ViewModels.Carts
{
    public class AddToCartDTO
    {
        public int UserId { get; set; }
        public List<GetCartItemDTO> Items { get; set; }
    }
}
