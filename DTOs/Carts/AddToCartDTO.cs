namespace E_Commerce.ViewModels.Carts
{
    public class AddToCartDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public GetCartProductDTO Product { get; set; }
    }
}
