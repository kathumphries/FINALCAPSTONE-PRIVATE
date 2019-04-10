using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models.EventViewModel
{
    public class EventViewModel
    {
        Event Event { get; set; }
        List<Genre> GenreList { get; set; }
        List<Venue> VenueList { get; set; }
    }
}
