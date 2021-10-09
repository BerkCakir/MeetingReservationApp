using AutoMapper;
using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Entities.Dtos;

namespace MeetingReservationApp.Managers.AutoMapper
{
    public class InventoryReservationProfile : Profile
    {
        public InventoryReservationProfile()
        {
            CreateMap<InventoryReservationAddDto, InventoryReservation>();
        }
    }
}
