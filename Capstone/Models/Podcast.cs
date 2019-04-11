using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Capstone.Models
{
    public class Podcast
    {
        public int PodcastID { get; set; }
        public int UserID { get; set; }
        public string Hosting { get; set; }
        public string URL { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public int GenreID { get; set; }
        // public bit SoundByte { get; set; }
        public DateTime OriginalRelease { get; set; }
        public string RunTime { get; set; }
        public string ReleaseFrequency { get; set; }
        public string AverageLength { get; set; }
        public int NumOfEpisodes { get; set; }
        public int NumOfDownloads { get; set; }
        public string MeasurementPlatform { get; set; }
        public string Demographics { get; set; }
        public string Affiliations { get; set; }

        [Required]
        public string BroadcastCity { get; set; }

        [Required]
        public string BroadcastState { get; set; }

       
        public bool InOhio {
            get
            {
                if (BroadcastState == "OH")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Required]
        public bool IsSponsored { get; set; }
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

