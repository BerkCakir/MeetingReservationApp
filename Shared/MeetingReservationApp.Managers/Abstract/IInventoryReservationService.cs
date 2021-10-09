using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Entities.Dtos;
using MeetingReservationApp.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Managers.Abstract
{
    public interface IInventoryReservationService
    {
        Task<IDataResult<IList<Inventory>>> GetAvailableInventories(AvailabilitySearchDto roomAvailabilitySearchDto, int locationId);
        Task<IResult> Add(InventoryReservationAddDto inventoryReservationAddDto, int locationId);
    }
}
