using E_Commerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace E_Commerce.Controller
{
    [ApiController]
    [Route("v1/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger<WebhookController> _logger;
        private readonly ECommerceDataContext _context;
        private readonly IConfiguration _config;

        public WebhookController(ILogger<WebhookController> logger,
            ECommerceDataContext context,
            IConfiguration config)
        {
            _logger = logger;
            _context = context;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var webhookSecret = _config["Stripe:WebhookSecret"];

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    webhookSecret
                );

                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                    string cartId = session.Metadata["cartId"];

                    var cart = await _context.Carts.FindAsync(int.Parse(cartId));
                    if (cart != null && !cart.Closed)
                    {
                        cart.Closed = true;
                        await _context.SaveChangesAsync();
                    }

                    _logger.LogInformation($"Confirmed Payment for cart {cartId}");
                }

                return Ok();
            }
            catch (StripeException e)
            {
                _logger.LogError(e, "Error at Stripe Webhook");
                return BadRequest();
            }
        }
    }
}
