using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }
        public string Podcast { get; set; }
        public string Venue { get; set; }
        public string Logo { get; set; }
        public string Copy { get; set; }
        public string PodcastURL { get; set; }
        public string TicketLevel { get; set; }
        public string UpsaleCopy { get; set; }
        [Display(Name = "Publish?")]
        public bool IsFinalized { get; set; }
        public string Name { get; set; }

    }
}
//using System.ComponentModel.DataAnnotations;
//[Display(Name = "Enter Your Name")]
//public string Username { get; set; }

//[Required]

//[DataType(DataType.PhoneNumber)]

//[DataType(DataType.EmailAddress)]

//[MinLength(8, ErrorMessage = "Password must be 8 characters or more")]

//[DataType(DataType.Password)]

//[Compare("Password")]

//[MaxLength(160)]

//[MinLength(5)]

//[DisplayName("Artist")]

//[Range(0.01, 100.00,
//    ErrorMessage = "Price must be between 0.01 and 100.00")]


