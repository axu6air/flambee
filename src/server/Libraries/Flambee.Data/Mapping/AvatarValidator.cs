//using Flambee.Core.Domain.Image;
//using FluentValidation;

//namespace Flambee.Data.Mapping
//{
//    public class AvatarValidator : AbstractValidator<Avatar>
//    {
//        public AvatarValidator()
//        {
//            RuleFor(x => x.Id).NotNull();
//            RuleFor(x => x.Title).MaximumLength(200);
//            RuleFor(x => x.ApplicationUser).SetValidator(new ApplicationUserValidator());
//        }
//    }
//}
