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
        private readonly IGenreSqlDal genreSqlDal;

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

        public IActionResult EditEvent(Event eventItem)
+        {
+            if (!ModelState.IsValid)
+            {
+                return View(eventItem);
+            }
+            else
+            {
+                bool result = eventSqlDal.AddEventDetail(eventItem);
+                //TODO : account for false
+                return RedirectToAction(nameof(ListOfEvents));
+            }
+        }


       
    }
}