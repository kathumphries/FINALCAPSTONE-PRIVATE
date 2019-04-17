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
        [Required]
        public int UserID { get; set; }//userID pk
        
        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }//email


        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [MaxLength(100)]
        public string Password { get; set; }//password

        public string Salt { get; set; } //salt
        

        public int Role { get; set; } //role fk
        [Required]
        [DisplayName("Name")]

        public string RoleDescription { get; set; }

        public string Name { get; set; }//name
        
        //[Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }//phoneNumber

        public string TicketLevel { get; set; }//ticketLevel
        public Ticket Ticket{ get; set; }

        public List<Event> EventList { get; set; }
    }
}

        //public bool IsAdmin { get; set; }
        //public bool IsProducer { get; set; }

