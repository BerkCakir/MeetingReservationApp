using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Shared.Data.Abstract;

namespace MeetingReservationApp.Data.Abstract
{
    public interface IInventoryReservationRepository : IEntityRepository<InventoryReservation>
    {
    }
}
