

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models.Account
{
    public class UserRoleAdminViewModel
    {
        public User User { get; set; }
        public List<SelectListItem> RoleList
        {
            get
            {
                List<SelectListItem> roleSelectList = new List<SelectListItem>();

                roleSelectList.Add(new SelectListItem("Administrator", "1"));
                roleSelectList.Add(new SelectListItem("Registered User", "2"));
                roleSelectList.Add(new SelectListItem("Podcaster", "3"));
                return roleSelectList;
            }
        }

    }
}


