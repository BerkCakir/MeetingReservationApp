using AutoMapper;
using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Managers.AutoMapper
{
    public class RoomReservationProfile : Profile
    {
        public RoomReservationProfile()
        {
            CreateMap<RoomReservationAddDto, RoomReservation>()
                .ForMember(dest => dest.MeetingStartTime, opt => opt.MapFrom(x =>
                           x.DesiredDate.Date.AddHours(x.StartHours).AddMinutes(x.StartMinutes)))
                .ForMember(dest => dest.MeetingEndTime, opt => opt.MapFrom(x =>
                           x.DesiredDate.Date.AddHours(x.EndHours).AddMinutes(x.EndMinutes)));
        }
    }
}
