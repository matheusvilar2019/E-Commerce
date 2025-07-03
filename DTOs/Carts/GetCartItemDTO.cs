using System.Text.Json.Serialization;

namespace E_Commerce.ViewModels.Carts
{
    public class GetCartItemDTO
    {
        [JsonIgnore]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public GetCartProductDTO Product { get; set; }
    }
}
