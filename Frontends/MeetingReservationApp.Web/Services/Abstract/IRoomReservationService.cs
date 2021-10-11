﻿using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Services.Abstract
{
    public interface IRoomReservationService
    {
        Task<IList<Room>> Get();
    }
}
