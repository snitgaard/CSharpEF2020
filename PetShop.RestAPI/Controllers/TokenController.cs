using Microsoft.AspNetCore.Mvc;
using System.Linq;
using PetShop.Core.Entity;
using PetShop.Infrastructure.Database;
using PetShop.Infrastructure.Database.Helpers;

namespace PetShop.RestAPI.Controllers
{
    [Route("/api/token")]
    [ApiController]
    public class TokenController : Controller
    {
        private IUserRepository<User> repository;
        private IAuthenticationHelper authenticationHelper;

        public TokenController(IUserRepository<User> repos, IAuthenticationHelper authHelper)
        {
            repository = repos;
            authenticationHelper = authHelper;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginInputModel model)
        {
            var user = repository.GetAll().FirstOrDefault(u => u.Username == model.Username);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!authenticationHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                token = authenticationHelper.GenerateToken(user)
            });
        }

    }
}