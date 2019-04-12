using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Models.ViewModel
{
    public class PodcastViewModel
    {
       public Podcast Podcast { get; set; }
       public List<SelectListItem>  GenreList { get; set; }

    }
}
