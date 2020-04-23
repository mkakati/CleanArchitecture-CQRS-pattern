using CleanArchitecture.Common.ApiResponse;
using CleanArchitecture.Common.Cryptography;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.DbContext;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static CleanArchitecture.Common.Enums.ResponseEnums;

namespace CleanArchitecture.Application.Account.Commands.Create.SignUp
{
    public class SignupCommandHandler : IRequestHandler<SignupCommand, ApiResponse>
    {

        private readonly CleanArchitectureDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SignupCommandHandler(CleanArchitectureDbContext context, UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<ApiResponse> Handle (SignupCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request.RegisterId != null || request.RegisterId != Guid.Empty)
                {

                    var ExistUser = _context.UserRegister.FirstOrDefault(x => x.UserName == request.UserName & x.IsActive == true);
                    if (ExistUser == null)
                    {
                        var userexist = _context.User.FirstOrDefault(x => x.UserName == request.UserName);
                        if (userexist == null)
                        {
                            UserRegister userRegister = new UserRegister
                            {
                                UserName = request.UserName,
                                FirstName = request.FirstName,
                                MiddleName = request.MiddleName,
                                LastName = request.LastName,
                                Email = request.Email,
                                Password = request.Password,
                                CreatedDate = DateTime.UtcNow,
                                PhoneNo = request.PhoneNo,

                                IsActive = true
                            };

                            await _context.AddAsync(userRegister);
                            //await _context.SaveChangesAsync();

                            var newUser = new ApplicationUser { UserName = request.UserName,
                                FirstName = request.FirstName, 
                                LastName = request.LastName, 
                                PhoneNumber = request.PhoneNo, 
                                //RegistrationNumber = request.RegistrationNumber, 
                                //PhoneNumberConfirmed = true, 
                                UserType = (int)UserRole.User };
                            await _roleManager.CreateAsync(new IdentityRole(UserRoleName.User.ToString()));
                            var objNew = await _userManager.CreateAsync(newUser, request.Password);
                            var id = newUser.Id;
                            await _userManager.AddClaimAsync(newUser, new Claim("Roles", UserRoleName.User.ToString()));
                            await _userManager.AddToRoleAsync(newUser, UserRoleName.User.ToString());

                            User user = new User();
                            user.UserName = request.UserName;
                            user.UserById = newUser.Id;
                            user.PhoneNo = request.PhoneNo;
                            //user.SSN = existrecord.SSN;
                            user.isLoginFirstTime = true;
                            user.Lat = 0;
                            user.Long = 0;
                            await _context.User.AddAsync(user);
                            _context.SaveChanges();

                            //ExistUser.IsActive = true;
                            //ExistUser.UpdatedDate = DateTime.UtcNow;
                            //_context.UserRegister.Update(ExistUser);
                            await _context.SaveChangesAsync();

                            response.Status = (int)Number.One;
                            response.Message = ResponseMessage.Success;
                            response.ResponseData = userRegister;

                        }
                        else
                        {
                            response.Status = (int)Number.One;
                            response.Message = ResponseMessage.UserExist;
                        }


                    }
                    else
                    {
                        response.Status = (int)Number.One;
                        response.Message = ResponseMessage.UserExist;

                    }
                }
                else
                {
                    var existrecord = _context.UserRegister.FirstOrDefault(x => x.RegisterId == request.RegisterId);
                    if (existrecord != null)
                    {
                        existrecord.FirstName = request.FirstName;
                        existrecord.LastName = request.LastName;
                        existrecord.MiddleName = request.MiddleName;
                        //existrecord.EmailId = request.EmailId;
                        existrecord.Password = RSAcrypotography.Encryption(request.Password);
                        existrecord.UpdatedDate = DateTime.UtcNow;
                        existrecord.PhoneNo = request.PhoneNo;
                        //existrecord.OTP = number;
                        //existrecord.OTPStartDateTime = DateTime.UtcNow;
                        //existrecord.OTPEndDateTime = DateTime.UtcNow.AddMinutes(5);

                        _context.Update(existrecord);
                        await _context.SaveChangesAsync();
                        //SendOTPMessage.SendMessage(request.PhoneNo, number);

                        response.Status = (int)Number.One;
                        response.Message = ResponseMessage.Success;
                        response.ResponseData = existrecord;
                    }
                    else
                    {
                        response.Status = (int)Number.Zero;
                        response.Message = ResponseMessage.PhoneExist;
                    }

                }
                
            }
            catch (Exception ex)
            {
                throw ex;

                //response.Status = (int)Number.Zero;
                //response.Message = ResponseMessage.Error;

            }
            return response;

        }
    }
}
