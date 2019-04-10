using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Capstone.Models;
using Capstone.DAL.Interfaces;

namespace Capstone.Controllers
{
    public class EventController : Controller
    {
        private readonly IPodcastSqlDal podcastDal;
        private readonly IEventSqlDal eventSqlDal;

        public EventController(IPodcastSqlDal podcastSqlDal, IEventSqlDal eventSqlDal)
        {
            this.podcastDal = podcastSqlDal;
            this.eventSqlDal = eventSqlDal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            EventViewModel model = new EventViewModel();

            List<Event> events = eventSqlDal.GetAllEvents();
            return View(events);
        }

       
    }
}