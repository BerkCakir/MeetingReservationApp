using MeetingReservationApp.Web.Models.InventoryReservation;
using MeetingReservationApp.Web.Models.RoomReservation;
using MeetingReservationApp.Web.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Services.Abstract
{
    public interface IInventoryReservationService
    {
        Task<IList<InventoryViewModel>> GetAll(int locationId);
        Task<Result> Add(InventoryReservationDto inventoryReservationDto);
    }
}
