using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Location { get; set; }
        public string LocationName { get => Location == 1 ? "Amsterdam" : "Berlin"; } // todo, must relocate to locationmanager class when its ready

        public IEnumerable<string> GetUserProps()
        {
            yield return UserName;
            yield return Email;
            yield return LocationName;
        }
    }
}
