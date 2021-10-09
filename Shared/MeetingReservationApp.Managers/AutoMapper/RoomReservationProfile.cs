using AutoMapper;
using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Entities.Dtos;

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
