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
        public string URL { get; set; } = "https://podfestmidwest.com/wp-content/uploads/2019/02/PodfestMidwest_Logo-FINAL-10.png";

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Genre Type")]
        public int GenreID { get; set; }
        public Genre Genre { get; set; }
        public DateTime OriginalRelease { get; set; } = DateTime.Now;
        public string RunTime { get; set; }
        public string ReleaseFrequency { get; set; } = "Monthly";
        public string AverageLength { get; set; } = "30 minutes";
        public int EpisodeCount { get; set; } = 0;
        public int DownloadCount { get; set; } = 0;
        public string MeasurementPlatform { get; set; } 
        public string Demographic { get; set; }
        public string Affiliations { get; set; }
        public string BroadcastCity { get; set; } 
        public string BroadcastState { get; set; }
      
        public bool InOhio
        {
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

        [Required] public bool IsSponsored { get; set; } = true;
        public string Sponsor { get; set; }
    }
}
