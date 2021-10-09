using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Shared.Utilities.Messages
{
    public static class Messages
    {
        public static class RoomReservation
        {
            public static string HoursNotAvailableForOffice()
            {
                return "Not any office available at desired time for your location";
            }
            public static string Success()
            {
                return "Room reservation added successfully";
            }
            public static string LocationHoursNotAvailable(string locationName)
            {
                return $"{locationName} Office is closed at desired time";
            }
            public static string AnotherMeetingExists()
            {
                return "Another meeting exists at the desired time interval";
            }
            public static string AttendantCountExceeds()
            {
                return "Requested attendant count is greater than the office's capacity";
            }
        }

        public static class InventoryReservation
        {
            public static string RelatedReservationNotExists()
            {
                return "Related room reservation not exists";
            }

            public static string Success()
            {
                return "Inventory reservation added successfully";
            }
            public static string HoursNotAvailableForInventory()
            {
                return "Not any inventory available at desired time for your location";
            }
            public static string AnotherReservationExists()
            {
                return "Another inventory reservation exists at the desired time interval";
            }
            public static string SamePurposeInventoryExists()
            {
                return "Another inventory exists for the same purpose for the meeting";
            }
        }
    }
}
