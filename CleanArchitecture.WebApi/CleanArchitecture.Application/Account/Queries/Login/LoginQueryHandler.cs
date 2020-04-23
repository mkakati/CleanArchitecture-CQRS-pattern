using CleanArchitecture.Application.Account.Models;
using CleanArchitecture.Common.ApiResponse;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.DbContext;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Account.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery , object>
    {
        private readonly CleanArchitectureDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public LoginQueryHandler()
        {

        }
        public LoginQueryHandler(CleanArchitectureDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<object> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                {
                    response.StatusCode = ResponseCode.BadRequest;
                    response.Message = ResponseMessage.UserNotExist;
                    return response;
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);


                if (!result.Succeeded)
                {
                    throw new Exception("Could not create token");
                }

                var userClaims = await _userManager.GetClaimsAsync(user);

                var roles = await _userManager.GetRolesAsync(user);
                userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
                userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                string k = _configuration["JwtKey"];
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(k));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

                var token = new JwtSecurityToken(
                  issuer: _configuration.GetSection("JwtIssuerOptions:Issuer").Value,
                  audience: _configuration.GetSection("JwtIssuerOptions:Audience").Value,
                  claims: userClaims,
                  expires: expires,
                  signingCredentials: creds
                );
                var userdetails = _context.User.FirstOrDefault(x => x.UserById == user.Id);

                UserLogin userloginmodel = new UserLogin();
                userloginmodel.Token = new JwtSecurityTokenHandler().WriteToken(token);
                userloginmodel.IsFirstTimeLogin = userdetails.isLoginFirstTime;
                userloginmodel.UserId = userdetails.UserById;
                userloginmodel.UserRole = user.UserType;
                userloginmodel.FName = user.FirstName;
                userloginmodel.LName = user.LastName;

                userdetails.Lat = request.Lat;
                userdetails.Long = request.Lng;
                userdetails.Token = userloginmodel.Token;
                _context.User.Update(userdetails);
                _context.SaveChanges();

                
                var firebasetoken = _context.storeDeviceTokens.FirstOrDefault(x => x.UserById == userdetails.UserById && x.TokenId == request.DiviceTokenId && x.IsDeleted == false);
                if (firebasetoken == null)
                {
                    StoreDeviceToken diviceToken = new StoreDeviceToken
                    {
                        TokenId = request.DiviceTokenId,
                        UserById = userdetails.UserById,
                        CreatedById = userdetails.UserById,
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true,
                        IsDeleted = false
                    };
                    _context.storeDeviceTokens.Add(diviceToken);
                    _context.SaveChanges();
                }

                response.StatusCode = ResponseCode.Ok;
                response.Message = ResponseMessage.Success;
                response.ResponseData = userloginmodel;
            }
            catch (Exception ex)
            {
                throw ex;
                //response.StatusCode = ResponseCode.BadRequest;
                //response.Message = ResponseMessage.InvalidPassword;
               
            }
            //var encryptor = new Encryptor();
            //var encrypted = encryptor.Encrypt(Encoding.Default.GetBytes(JsonConvert.SerializeObject(response)), RNCryptorKey.Secretkey);
            //string encryptcode = Convert.ToBase64String(encrypted);

            //object responseObj = new
            //{
            //    responseData = encryptcode
            //};
            return response;
        }

    }
}
