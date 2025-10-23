using E_Commerce.Data;
using E_Commerce.Model;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

namespace E_Commerce.Controller
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PaymentController : ControllerBase
    {
        [HttpPost("create-checkout-session/{userId:int}")]
        public async Task<IActionResult> CreateCheckoutSession([FromRoute] int userId, [FromServices] ECommerceDataContext context)
        {
            try
            {
                var cart = await context
                    .Carts
                    .FirstOrDefaultAsync(c => c.UserId == userId && !c.Closed);

                decimal subTotalValue = await context.Carts
                .Where(c => c.UserId == userId)
                .SelectMany(c => c.Items)
                .SumAsync(i => i.Product.Price * i.Quantity);

                decimal taxEstimate = subTotalValue / 10; // 10%
                decimal shippingEstimate = subTotalValue / 50; // 20%
                long totalValue = Convert.ToInt64((subTotalValue + taxEstimate + shippingEstimate) * 100);

                if (cart == null) return NotFound();

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                {
                    new()
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = totalValue,
                            Currency = "brl",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "E-Commerce Payment (Test)"
                            }
                        },
                        Quantity = 1
                    }
                },
                    Mode = "payment",
                    SuccessUrl = "http://localhost:4200/success",
                    CancelUrl = "http://localhost:4200",
                    Metadata = new Dictionary<string, string>
                {
                    { "cartId", cart.Id.ToString() }
                }
                };

                var service = new SessionService();
                var session = service.Create(options);

                return Ok(new { url = session.Url });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultDTO<Cart>("PYT-101 - Internal server failure"));
            }
        }
    }
}
