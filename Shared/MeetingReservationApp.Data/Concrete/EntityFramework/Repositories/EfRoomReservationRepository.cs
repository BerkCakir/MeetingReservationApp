﻿using MeetingReservationApp.Data.Abstract;
using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Data.Concrete.EntityFramework.Repositories
{
    class EfRoomReservationRepository : EfEntityRepositoryBase<RoomReservation>, IRoomReservationRepository
    {
        public EfRoomReservationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
