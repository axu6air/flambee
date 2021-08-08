using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.User;
using Flambee.WebAPI.Models.User;
using AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<ApplicationUser, UserModel>();
        CreateMap<UserInfo, UserInfoModel>();
    }
}