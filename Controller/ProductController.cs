using E_Commerce.Data;
using E_Commerce.Extensions;
using E_Commerce.Model;
using E_Commerce.ViewModels;
using E_Commerce.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace E_Commerce.Controller
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("v1/products")]
        public async Task<IActionResult> GetAsync([FromServices] ECommerceDataContext context)
        {
            try
            {
                var products = await context.Products.ToListAsync();
                return Ok(new ResultViewModel<List<Product>>(products));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<Product>>("PDT-101 - Internal server failure"));
            }
        }

        [HttpGet("v1/products/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] ECommerceDataContext context)
        {
            try
            {
                var product = await context
                .Products
                .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null) return NotFound();

                return Ok(new ResultViewModel<Product>(product));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("PDT-201 - Internal server failure"));
            }
            
        }

        [HttpPost("v1/products")]
        public async Task<IActionResult> PostAsync([FromBody] EditorProductViewModel model, [FromServices] ECommerceDataContext context)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<Product>(ModelState.GetErrors()));
            try
            {
                Product product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    Slug = model.Slug,
                    Image = model.Image
                };

                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();

                return Created($"v1/products/{product.Id}", new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("PDT-301 - Could not include product"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("PDT-302 - Internal server failure"));
            }
        }

        [HttpPut("v1/products/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorProductViewModel model, [FromServices] ECommerceDataContext context)
        {
            try
            {
                var product = await context
                .Products
                .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null) return NotFound();

                product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;
                product.Slug = model.Slug;
                product.Image = model.Image;

                context.Products.Update(product);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("PDT-401 - Could not update product"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("PDT-405 - Internal server failure"));
            }
        }

        [HttpDelete("v1/products/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] ECommerceDataContext context)
        {
            try
            {
                var product = await context
                .Products
                .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null) return NotFound();

                context.Products.Remove(product);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Product>(product));
            }         
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("PDT-501 - Could not delete product"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Product>("PDT-502 - Internal server failure"));
            }
        }
    }
}
