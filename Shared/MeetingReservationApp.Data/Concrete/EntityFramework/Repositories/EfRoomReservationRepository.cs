using MeetingReservationApp.Data.Abstract;
using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace MeetingReservationApp.Data.Concrete.EntityFramework.Repositories
{
    class EfRoomReservationRepository : EfEntityRepositoryBase<RoomReservation>, IRoomReservationRepository
    {
        public EfRoomReservationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
