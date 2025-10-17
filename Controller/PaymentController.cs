using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace E_Commerce.Controller
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PaymentController : ControllerBase
    {
        [HttpPost("create-checkout-session/{id:int}")]
        public ActionResult CreateCheckoutSession([FromRoute] int id)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = 2000, // $20.00
                        Currency = "brl",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Produto de Teste"
                        }
                    },
                    Quantity = 1
                }
            },
                Mode = "payment",
                SuccessUrl = "https://localhost:5001/success",
                CancelUrl = "https://localhost:5001/cancel",
                Metadata = new Dictionary<string, string>
            {
                { "cartId", id.ToString() } // exemplo fixo, ou vindo do seu banco
            }
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Ok(new { url = session.Url });
        }
    }
}
