using E_Commerce.Data;
using E_Commerce.DTOs.Carts;
using E_Commerce.Model;
using E_Commerce.ViewModels;
using E_Commerce.ViewModels.Carts;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace E_Commerce.Controller
{
    [ApiController]
    public class CartController : ControllerBase
    {
        [HttpGet("v1/carts")]
        public async Task<IActionResult> GetCartAsync([FromServices] ECommerceDataContext context)
        {
            try
            {
                var carts = await context.Carts.ToListAsync();
                return Ok(new ResultDTO<List<Cart>>(carts));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultDTO<List<Cart>>("CRT-101 - Internal server failure"));
            }
        }

        [HttpGet("v1/carts/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] ECommerceDataContext context)
        {
            try
            {
                var cart = await context.Carts.FirstOrDefaultAsync(x => x.Id == id);
                return Ok(new ResultDTO<Cart>(cart));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultDTO<Cart>("CRT-102 - Internal server failure"));
            }
        }

        [HttpGet("v1/carts/items/user/{userId:int}")]
        public async Task<IActionResult> GetCartByUserIdAsync([FromServices] ECommerceDataContext context, [FromRoute] int userId)
        {
            try
            {
                var cart = await context
                    .Carts
                    .Include(c => c.Items)
                        .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                var cartDTO = new GetCartDTO
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
                    Items = cart.Items.Select(item => new GetCartItemDTO
                    {
                        Id = item.Id,
                        Quantity = item.Quantity,
                        Product = new GetCartProductDTO
                        {
                            Id = item.Product.Id,
                            Name = item.Product.Name,
                            Price = item.Product.Price,
                            Description = item.Product.Description,
                            Slug = item.Product.Slug,
                            Image = item.Product.Image
                        }
                    }).ToList()
                };

                return Ok(new ResultDTO<GetCartDTO>(cartDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultDTO<Cart>("CRT-103 - Internal server failure"));
            }
        }

        [HttpPost("v1/carts/items/user/{userId:int}")]
        public async Task<IActionResult> PostCartItemsAsync([FromRoute] int userId, [FromBody] List<AddToCartDTO> requestItems, [FromServices] ECommerceDataContext context)
        {
            try
            {
                var cart = await context.Carts
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                {
                    cart = new Cart { UserId = userId };
                    await context.Carts.AddAsync(cart);
                }

                foreach (var item in requestItems)
                {
                    var product = await context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                    if (product == null) continue;

                    var existingItem = cart.Items != null ? cart.Items.FirstOrDefault(i => i.ProductId == product.Id) : null;
                    if (existingItem != null)
                    {
                        existingItem.Quantity = item.Quantity;
                    }
                    else
                    {
                        cart.Items.Add(new CartItem
                        {
                            ProductId = product.Id,
                            Quantity = item.Quantity
                        });
                    }
                }

                await context.SaveChangesAsync();
                return Ok(new ResultDTO<List<AddToCartDTO>>(requestItems));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "CRT-104 - Internal server failure");
            }
        }

        [HttpPatch("v1/carts/{userId:int}")]
        public async Task<IActionResult> PatchByIdAsync([FromRoute] int userId, [FromBody] bool closed, [FromServices] ECommerceDataContext context)
        {
            try
            {
                var cart = await context.Carts.FirstOrDefaultAsync(x => x.UserId == userId);

                if (cart == null) return NotFound();

                cart.Closed = closed;
                context.Carts.Update(cart);
                await context.SaveChangesAsync();

                return Ok(new ResultDTO<Cart>(cart));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultDTO<Cart>("CRT-105 - Internal server failure"));
            }
        }

        [HttpDelete("v1/carts/{id:int}")]
        public async Task<IActionResult> DeleteCartAsync([FromRoute] int id, [FromServices] ECommerceDataContext context)
        {
            try
            {
                var cart = await context
                    .Carts
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (cart == null) return NotFound();

                context.Carts.Remove(cart);
                await context.SaveChangesAsync();

                return Ok(new ResultDTO<Cart>(cart));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultDTO<Cart>("CRT-106 - Internal server failure"));
            }

        }

        [HttpDelete("v1/carts/items/{id:int}")]
        public async Task<IActionResult> DeleteCartItemAsync([FromRoute] int id, [FromServices] ECommerceDataContext context)
        {
            try
            {
                var cartItem = await context
                    .CartItem
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (cartItem == null) return NotFound();

                context.CartItem.Remove(cartItem);
                await context.SaveChangesAsync();

                return StatusCode(500, new ResultDTO<int>(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultDTO<Cart>("CRT-106 - Internal server failure"));
            }
        }
    }
}
