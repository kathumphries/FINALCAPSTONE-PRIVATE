using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Capstone.Models
{
    public class Podcast
    {
        public int PodcastId { get; set; }
        public int UserId { get; set; }
        public string Hosting { get; set; }
        public string URL { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int GenreId { get; set; }
        // public bit SoundByte { get; set; }
        public DateTime OriginalRelease { get; set; }
        public double RunTime { get; set; }
        public string ReleaseFrequency { get; set; }
        public double AverageLength { get; set; }
        public int NumOfEpisodes { get; set; }
        public int NumOfDownloads { get; set; }
        public string MeasurementPlatform { get; set; }
        public string Demographics { get; set; }
        public string Affiliations { get; set; }
        public string broadcastCity { get; set; }
        public string broadcastState { get; set; }
        public bool InOhio { get; set; }
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

