using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Models
{
    public class ServiceApiSettings
    {
        public string IdentityBaseUri { get; set; }
        public string GatewayBaseUri { get; set; }
        public ServiceApi RoomReservation { get; set; }
        public ServiceApi InventoryReservation { get; set; }
    }

    public class ServiceApi
    {
        public string Path { get; set; }
    }
}
