using E_Commerce.Data;
using E_Commerce.Model;
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
            var product = await context.Products.ToListAsync();
            return Ok(product);
        }

        [HttpGet("v1/products/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] ECommerceDataContext context)
        {
            var product = await context
                .Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost("v1/products")]
        public async Task<IActionResult> PostAsync([FromBody] EditorProductViewModel model, [FromServices] ECommerceDataContext context)
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

            return Created($"v1/products/{product.Id}", product);
        }

        [HttpPut("v1/products/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorProductViewModel model, [FromServices] ECommerceDataContext context)
        {
            var product = await context
                .Products
                .FirstOrDefaultAsync (p => p.Id == id);

            if (product == null) return NotFound();

            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Slug = model.Slug;
            product.Image = model.Image;

            context.Products.Update(product);
            await context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("v1/products/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] ECommerceDataContext context)
        {
            var product = await context
                .Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return Ok(product);            
        }
    }
}
