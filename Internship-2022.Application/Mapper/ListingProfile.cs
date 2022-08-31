using AutoMapper;
using Internship_2022.Application.Models.ListingDtos;
using Internship_2022.Domain.Entities;
using Internship_2022.Domain.Enum;
using Microsoft.Extensions.Configuration;

namespace Internship_2022.Application.Mapper
{
    public class ListingProfile : Profile
    {
        private readonly string SASKey;

        public ListingProfile()
        {
            SASKey = "?sv=2021-06-08&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2024-09-01T15:28:12Z&st=2022-08-22T07:28:12Z&spr=https&sig=NTVo%2FNeLzQReKQqfbxrS5rsdV%2FZMk8YhATV8J2EIRDs%3D";

            CreateMap<CreateListingRequestDto, Listing>()
                    .ForMember(destination => destination.Title,
               map => map.MapFrom(
                   source => source.Title))
                     .ForMember(destination => destination.Category,
                map => map.MapFrom(
                    source => Enum.Parse(typeof(ECategory),source.Category)))
                     .ForMember(destination => destination.Price,
                map => map.MapFrom(
                    source => source.Price))
                     .ForMember(destination => destination.Images,
                map => map.MapFrom(
                    source => string.Join(",", source.Images)))
                     .ForMember(destination => destination.Description,
                map => map.MapFrom(
                    source => source.Description))
                     .ForMember(destination => destination.Location,
                map => map.MapFrom(
                    source => source.Location))
                     .ForMember(destination => destination.Phone,
               map => map.MapFrom(
                   source => source.Phone));

            CreateMap<Listing, GetListingByIdRequestDto>()
                   .ForMember(destination => destination.Title,
              map => map.MapFrom(
                  source => source.Title))
                    .ForMember(destination => destination.Category,
               map => map.MapFrom(
                   source => source.Category.ToString()))
                    .ForMember(destination => destination.Price,
               map => map.MapFrom(
                   source => source.Price))
                    .ForMember(destination => destination.Description,
               map => map.MapFrom(
                   source => source.Description))
                    .ForMember(destination => destination.Location,
               map => map.MapFrom(
                   source => source.Location))
                    .ForMember(destination => destination.Phone,
               map => map.MapFrom(
                   source => source.Phone))
                    .ForMember(destination => destination.Id,
               map => map.MapFrom(
                   source => source.Id))
                      .ForMember(destination => destination.Images,
               map => map.MapFrom(
                   source => source.Images.Split(",", StringSplitOptions.None).ToList().Select(e=>e+SASKey).ToArray()));


            CreateMap<Listing, GetListingRequestDto>()
                  .ForMember(destination => destination.Title,
             map => map.MapFrom(
                 source => source.Title))
                   .ForMember(destination => destination.Category,
              map => map.MapFrom(
                 source => source.Category.ToString()))
                   .ForMember(destination => destination.Price,
              map => map.MapFrom(
                  source => source.Price))
                   .ForMember(destination => destination.Description,
              map => map.MapFrom(
                  source => source.Description))
                   .ForMember(destination => destination.Location,
              map => map.MapFrom(
                  source => source.Location))
                    .ForMember(destination => destination.Phone,
               map => map.MapFrom(
                   source => source.Phone))
                    .ForMember(destination => destination.Id,
               map => map.MapFrom(
                   source => source.Id))
               .ForMember(destination => destination.Images,
               map => map.MapFrom(
                   source => source.Images.Split(",", StringSplitOptions.None).ToList().Select(e => e + SASKey).ToArray()));

            CreateMap<UpdateListingRequestDto, Listing>()
                 .ForMember(destination => destination.Title,
            map => map.MapFrom(
                source => source.Title))
                  .ForMember(destination => destination.Category,
             map => map.MapFrom(
                 source => Enum.Parse(typeof(ECategory), source.Category)))
                  .ForMember(destination => destination.Price,
             map => map.MapFrom(
                 source => source.Price))
                  .ForMember(destination => destination.Description,
             map => map.MapFrom(
                 source => source.Description))
                  .ForMember(destination => destination.Location,
             map => map.MapFrom(
                 source => source.Location))
                  .ForMember(destination => destination.Phone,
               map => map.MapFrom(
                   source => source.Phone))
                       .ForMember(destination => destination.Images,
                map => map.MapFrom(
                    source => string.Join(",", source.Images)));


        }
    }
}
