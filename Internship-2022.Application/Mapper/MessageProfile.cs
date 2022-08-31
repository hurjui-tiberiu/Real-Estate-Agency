using AutoMapper;
using Internship_2022.Application.Models.MessageDto;
using Internship_2022.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2022.Application.Mapper
{
    public class MessageProfile:Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageRequestDto, Message>()
                    .ForMember(destination => destination.SenderId,
               map => map.MapFrom(
                   source => source.SenderId))
                     .ForMember(destination => destination.ReceiverId,
                map => map.MapFrom(
                    source => source.ReceiverId))
                     .ForMember(destination => destination.Content,
                map => map.MapFrom(
                    source => source.Content))
              .ForMember(destination => destination.ListingId,
                map => map.MapFrom(
                    source => source.ListingId));

            CreateMap<Message, MessageRequestDto>()
                   .ForMember(destination => destination.SenderId,
              map => map.MapFrom(
                  source => source.SenderId))
                    .ForMember(destination => destination.ReceiverId,
               map => map.MapFrom(
                   source => source.ReceiverId))
                    .ForMember(destination => destination.Content,
               map => map.MapFrom(
                   source => source.Content))
             .ForMember(destination => destination.ListingId,
               map => map.MapFrom(
                   source => source.ListingId));

        }

    }
}
