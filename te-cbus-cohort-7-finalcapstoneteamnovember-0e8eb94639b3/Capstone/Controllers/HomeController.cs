﻿using System;
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
    public class HomeController : Controller
    {

        private readonly IPodcastSqlDal podcastSqlDal;
        private readonly IEventSqlDal eventSqlDal;          


        public HomeController(IPodcastSqlDal podcastSqlDal, IEventSqlDal eventSqlDal)
        {
            this.podcastSqlDal = podcastSqlDal;
            this.eventSqlDal = eventSqlDal;
        }


        public IActionResult Index()
        {
            return View();
        }

       


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}



//VALIDATION:
//using System.ComponentModel.DataAnnotations;

//<<1st thing in Controller Method>> 


//if (!ModelState.IsValid)
//{
//// Return the new view again
//return View(city);
//}
//else
//{
//// Save the City
//cityDao.AddCity(city);

//// Redirect the user to City/Index Action
//return RedirectToAction("index", "city");
//}
//good use is to return to a Search screen where search Model had issues..
//return View("Search", searchModel); might want to tag anything in the model[Required] that you will be passing back.

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~

//SESSION:
//HttpContext.Session.SetString(key, value);
//To retrieve data from session, type the following:

//string value = HttpContext.Session.GetString(key);
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~

//TEMP DATA
//It is only useful when you are redirecting the user and want the data to still be available.Once you access the data stored in TempData, it is automatically cleared.

//To set information in TempData, write the following in your Controller before performing a redirect:

//TempData["Key"] = value; // Where Key is a string and Value is any Object
//To get information out of TempData in the subsequent request:

//// Get the Value using the same Key
//// You may need to cast it
//var value = TempData["Key"];

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//RedirectToAction("Action", new { id = 99 }); This will cause a redirect to Site/Controller/Action/99.

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
