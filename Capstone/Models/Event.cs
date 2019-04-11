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
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }
        // public string PodcastName { get; set; }

        public string PodcastID { get; set; }

        public Podcast Podcast { get; set; }

        public string VenueID { get; set; }

        [Display(Name = "Cover Photo URL (https://bobo.com)")]
        public string CoverPhoto { get; set; }

        [Display(Name = "Description")]
        public string DescriptionCopy { get; set; }

        // public string PodcastURL { get; set; }
        [Display(Name = "Level")]
        public string TicketLevel { get; set; }

        [Display(Name = "Upsale Copy")]
        public string UpsaleCopy { get; set; }

        [Display(Name = "Published")]
        public bool IsFinalized { get; set; }

        [Display(Name = "Event Name")]
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


