using Flambee.Core.Domain.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Core.Data.Mapping
{
    public class AvatarValidator : AbstractValidator<Avatar>
    {
        public AvatarValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Title).MaximumLength(200);
        }
    }
}
