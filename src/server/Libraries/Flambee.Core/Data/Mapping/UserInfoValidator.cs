using Flambee.Core.Domain.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Core.Data.Mapping
{
    public class UserInfoValidator : AbstractValidator<UserInfo>
    {
        public UserInfoValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.FirstName).Length(0, 30).NotEmpty().WithMessage("First Name is required!");
            RuleFor(x => x.LastName).Length(0, 30).Empty();
            RuleFor(x => x.DateOfBirth).Empty();
            RuleFor(x => x.PhoneNumber).Length(14).NotEmpty().WithMessage("Phone Number is required!").Matches(@"^(?:\+88|01)?\d{11}\r?$").WithMessage("Invalid Phone Number!");
            RuleFor(x => x.ApplicationUser).SetValidator(new ApplicationUserValidator());
        }
    }
}
