using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models.ViewModel
{
    public class AdminViewModel
    {
        public List<User> UserList { get; set; }
        public List<User> AdminList { get; set; }
        public List<User> PodcasterList { get; set; }
        public int RoleType { get; set; }
        //public  List<Availability> PodcasterSchedule {get; set;}
       //public List<Log> LogList { get; set; }

    }

   
}
