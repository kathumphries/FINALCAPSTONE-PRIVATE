using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [DisplayName("EventName")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsProducer { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string TicketLevel { get; set; }

        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        public string Hash { get; set; }
        
    }
}


//using System.ComponentModel.DataAnnotations;
//[Display(EventName = "Enter Your EventName")]
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
