using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.UserDetails;
using Flambee.WebAPI.Models.User;
using AutoMapper;
using Flambee.WebAPI.Models.Avatar;
using Flambee.Core.Domain.Image;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<User, UserModel>().ReverseMap();
        CreateMap<UserInfo, UserInfoModel>().ReverseMap();
        CreateMap<Avatar, AvatarModel>().ReverseMap();
    }
}