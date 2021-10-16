using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Entities.Dtos;
using MeetingReservationApp.Shared.Utilities.Results.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingReservationApp.Managers.Abstract
{
    public interface IInventoryReservationService
    {
        Task<IDataResult<IList<Inventory>>> GetAll(int locationId);
        Task<IResult> Add(InventoryReservationAddDto inventoryReservationAddDto);
    }
}
