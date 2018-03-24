using System;
namespace WaldoGOP.Models
{
    public class Event
    {
        public int eventID { get; set; }

        public string eventName { get; set;  }

        public string eventLocation { get; set; }

        public DateTime eventDate { get; set; }

        public string eventLink { get; set; }

        public string eventLocationName { get; set; }

        public string eventDescription { get; set; }

        public int eventDuration { get; set; }
    }
}
