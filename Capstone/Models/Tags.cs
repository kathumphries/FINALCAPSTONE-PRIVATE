using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Tags
    {
        public int TagId { get; set; }
        public int GenreId { get; set; }
        public string TicketLevel { get; set; }
        public int VenueId { get; set; }
        public bool IsVisible { get; set; }
        public string TagDescription { get; set; }
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

