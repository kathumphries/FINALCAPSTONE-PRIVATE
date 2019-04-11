using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class PodcasterAvailability
    {
        public int UserID { get; set; }
        public int EventID { get; set; }
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }
        public string Description { get; set; }
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
