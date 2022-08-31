using AutoMapper;
using Internship_2022.Application.Models.UserDto;
using Internship_2022.Domain.Entities;

namespace Internship_2022.Application.Mapper
{
    public class UserProfile : Profile
    {
        const string SASKey = "?sv=2021-06-08&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2024-09-01T15:28:12Z&st=2022-08-22T07:28:12Z&spr=https&sig=NTVo%2FNeLzQReKQqfbxrS5rsdV%2FZMk8YhATV8J2EIRDs%3D";

        public UserProfile()
        {
            CreateMap<CreateUserRequestDto, User>()
                .ForMember(src => src.Email,
            map => map.MapFrom(
                dest => dest.Email))
                .ForMember(src => src.Password,
            map => map.MapFrom
                (dest => dest.Password));

            CreateMap<User, UserRequestDto>()
                .ForMember(src => src.Photo,
            map => map.MapFrom(
                dest => dest.Photo+SASKey))
                .ForMember(src => src.FullName,
            map => map.MapFrom
                (dest => dest.FullName))
                .ForMember(src => src.Gender,
            map => map.MapFrom
                (dest => dest.Gender))
                .ForMember(src => src.DateOfBirth,
            map => map.MapFrom
                (src => src.DateOfBirth))
                .ForMember(src => src.Mail,
            map => map.MapFrom
                 (dest => dest.Email))
                .ForMember(src => src.Phone,
           map=>map.MapFrom
                (dest => dest.Phone));

            CreateMap<UpdateRequestDto, User>()
               .ForMember(src => src.Photo,
           map => map.MapFrom(
               dest => dest.Photo))
               .ForMember(src => src.FullName,
           map => map.MapFrom
               (dest => dest.FullName))
               .ForMember(src => src.Gender,
           map => map.MapFrom
               (dest => dest.Gender))
               .ForMember(src => src.DateOfBirth,
           map => map.MapFrom
               (src => src.DateOfBirth))
               .ForMember(src => src.Email,
           map => map.MapFrom
                (dest => dest.Mail))
               .ForMember(src => src.Phone,
          map => map.MapFrom
               (dest => dest.Phone));


            CreateMap<PatchUserRequestDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
