using E_Commerce.Data;
using E_Commerce.Extensions;
using E_Commerce.Model;
using E_Commerce.Services;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace E_Commerce.Controller
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TokenService _tokenService;
        public AccountController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model, [FromServices] ECommerceDataContext context, [FromServices] TokenService tokenService) 
        {
            if (!ModelState.IsValid) return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));

            var user = await context
                .Users
                .AsNoTracking()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null) 
                return StatusCode(401, new ResultDTO<string>("Usuário ou senha inválidos"));

            if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
                return StatusCode(401, new ResultDTO<string>("Usuário ou senha inválidos"));

            try
            {
                var token = tokenService.GenerateToken(user);
                return Ok(new ResultDTO<string>(token, null));
            }
            catch
            {
                return StatusCode(500, new ResultDTO<string>("ACC-101 - Falha interna no servidor"));
            }
        }

        [HttpPost("v1/accounts")]
        public async Task<IActionResult> Post([FromBody] RegisterDTO model, [FromServices] ECommerceDataContext context)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = PasswordHasher.Hash(model.Password),
                CPF = model.CPF,
                BirthDate = model.BirthDate,
                CEP = model.CEP,
                Address = model.Address
            };

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return Ok(new ResultDTO<dynamic>(new
                {
                    user = user.Email, user.PasswordHash
                }));
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, new ResultDTO<string>("ACC-101 - E-mail already exists"));
            }
            catch(Exception)
            {
                return StatusCode(500, new ResultDTO<string>("ACC-102 - Internal server error"));
            }
        }

        [HttpGet("v1/emailExists/{email}")]
        public async Task<IActionResult> EmailExistsAsync([FromRoute] string email, [FromServices] ECommerceDataContext context)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null) return Ok(false);

                bool emailExists = user.Email == email;
                return Ok(emailExists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultDTO<string>("ACC-201 - Internal server error"));
            }
        }
    }
}
