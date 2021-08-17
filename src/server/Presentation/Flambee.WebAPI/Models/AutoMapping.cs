using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.User;
using Flambee.WebAPI.Models.User;
using AutoMapper;
using Flambee.WebAPI.Models.Avatar;
using Flambee.Core.Domain.Image;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<ApplicationUser, UserModel>().ReverseMap();
        CreateMap<UserInfo, UserInfoModel>().ReverseMap();
        CreateMap<Avatar, AvatarModel>().ReverseMap();
    }
}