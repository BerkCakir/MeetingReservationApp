using MeetingReservationApp.Data.Abstract;
using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace MeetingReservationApp.Data.Concrete.EntityFramework.Repositories
{
    public class EfInventoryReservationRepository : EfEntityRepositoryBase<InventoryReservation>, IInventoryReservationRepository
    {
        public EfInventoryReservationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
