using App.Core.Domain.Authentication;
using App.Core.Domain.User;
using App.WebAPI.Models.User;
using AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<ApplicationUser, UserModel>();
        CreateMap<UserInfo, UserInfoModel>();
    }
}