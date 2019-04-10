using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models.ViewModel
{
    public class EventViewModel
    {
        public Event EventItem { get; set; }
        public List<SelectListItem> GenreList { get; set; }
        public List<SelectListItem> VenueList { get; set; }
    }
}
