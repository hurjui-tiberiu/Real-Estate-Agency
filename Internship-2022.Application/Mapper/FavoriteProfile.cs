using AutoMapper;
using Internship_2022.Application.Models.FavoriteDto;
using Internship_2022.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2022.Application.Mapper
{
    public class FavoriteProfile:Profile
    {
        public FavoriteProfile()
        {
            CreateMap<Favorite, FavoriteRequestDto>()
                    .ForMember(destination => destination.UserId,
               map => map.MapFrom(
                   source => source.UserId))
                     .ForMember(destination => destination.ListingId,
                map => map.MapFrom(
                    source => source.ListingId));
        }
    }
}
