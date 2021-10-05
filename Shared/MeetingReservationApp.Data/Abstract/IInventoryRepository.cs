using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Data.Abstract
{
    public interface IInventoryRepository : IEntityRepository<Inventory>
    {
    }
}
