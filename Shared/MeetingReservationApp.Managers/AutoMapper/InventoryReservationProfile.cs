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
    public class InventoryReservationProfile : Profile
    {
        public InventoryReservationProfile()
        {
            CreateMap<InventoryReservationAddDto, InventoryReservation>();
        }
    }
}
