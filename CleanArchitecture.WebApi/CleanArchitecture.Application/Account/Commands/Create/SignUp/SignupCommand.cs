using CleanArchitecture.Common.ApiResponse;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CleanArchitecture.Application.Account.Commands.Create.SignUp
{
    public class SignupCommand : IRequest<ApiResponse>
    {
        public Guid? RegisterId { get; set; }
        public string UserName { get; set; }
        
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        //public string SSN { get; set; }
    }

    public class CreateSplitPayUserCommandValidator : AbstractValidator<SignupCommand>
    {
        public CreateSplitPayUserCommandValidator()
        {
            RuleFor(req => req.Email).NotEmpty().WithMessage("Email cannot be empty");
           
        }
    }
}
