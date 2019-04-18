using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public int Day { get; set; }
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }

        public DateTime PodfestStartTime { get; set; } = new DateTime(2019, 8, 23);
        // public string PodcastName { get; set; }

        public string PodcastID { get; set; }
        public Podcast Podcast { get; set; } //will pull details from db but wont store. 

        [Display(Name = "Venue Location")] public string VenueID { get; set; }
        public Venue Venue { get; set; }

        [Display(Name = "Cover Photo URL (https://bobo.com)")]
        public string CoverPhoto { get; set; }

        [Display(Name = "Description")] public string DescriptionCopy { get; set; }

        // public string PodcastURL { get; set; }

        [Display(Name = "Ticket Level")] public string TicketLevel { get; set; } //TODO: change to int rename TicketID
        public Ticket Ticket { get; set; }

        [Display(Name = "Upsale Copy")] public string UpsaleCopy { get; set; }

        [Display(Name = "Published")] public bool IsFinalized { get; set; }

        [Display(Name = "Event Title")] public string EventName { get; set; }

        public iCalendar ICalendar { get; set; }

        public string TimeOfDayString { get; set; }

    }



}
