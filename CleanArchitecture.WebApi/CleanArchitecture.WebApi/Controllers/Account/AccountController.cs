using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CleanArchitecture.Application.Account.Commands.Create.SignUp;
using CleanArchitecture.Application.Account.Queries.Login;
using CleanArchitecture.Application.ValidateToken;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.DbContext;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CleanArchitecture.WebApi.Controllers.Account
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CleanArchitectureDbContext _dbContext;
        private readonly JsonSerializerSettings _serializerSettings;

        public AccountController(UserManager<ApplicationUser> userManager, CleanArchitectureDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        [Authorize]
        private async Task<dynamic> CheckTokenQueryAsync()
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            dynamic res = await Mediator.Send(new ValidateTokenQuery()
            {
                UserId = user.Id
            });
            if (res.StatusCode == 200)
                return user.Id;
            else return res;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp ([FromBody] SignupCommand model)
        {
            return Ok(await Mediator.Send(model));
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginQuery model)
        {
            return Ok(await Mediator.Send(model));
        }

    }
}