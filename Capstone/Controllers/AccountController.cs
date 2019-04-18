using Capstone.DAL.Interfaces;
using Capstone.Models;
using Capstone.Models.Account;
using Capstone.Providers.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Capstone.Providers.Auth;
using System.Text;
using System;
using Capstone.Models.ViewModel;

namespace Capstone.Controllers
{
    public class AccountController : Controller

    {
        const string SessionName = "User_Auth";
        
        private readonly IAuthProvider authProvider;
        private readonly IUserSqlDal userSqlDal;
        private readonly IPodcastSqlDal podcastSqlDal;
        private readonly IEventSqlDal eventSqlDal;
        private readonly IGenreSqlDal genreSqlDal;
        private readonly IVenueSqlDal venueSqlDal;
        private readonly ITicketSqlDal ticketSqlDal;
        private readonly IUserEventSqlDal userEventSqlDal;



        public AccountController(IAuthProvider authProvider, IUserSqlDal userSqlDal, IEventSqlDal eventSqlDal, ITicketSqlDal ticketSqlDal) 
            //IPodcastSqlDal podcastSqlDal, IGenreSqlDal genreSqlDal, IVenueSqlDal venueSqlDal, ITicketSqlDal ticketSqlDal)
        {
            this.authProvider = authProvider;
            this.userSqlDal = userSqlDal;
            //this.podcastSqlDal = podcastSqlDal;
            this.eventSqlDal = eventSqlDal;
            //this.genreSqlDal = genreSqlDal;
            //this.venueSqlDal = venueSqlDal;
            this.ticketSqlDal = ticketSqlDal;
        }

        //Roles in db: 
        //("1" - Admin, "2 - RegisterUser", "3 - Podcaster", "4 - Anonymous"

        //[AuthorizationFilter] // actions can be filtered to only those that are logged in
   
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            // Ensure the fields were filled out
            if (ModelState.IsValid)
            {
                // Check that they provided correct credentials
                bool validLogin = authProvider.SignIn(loginViewModel.Email, loginViewModel.Password);
                if (validLogin)
                {
                    // Redirect the user where you want them to go after successful login
                    return RedirectToAction("Index", "Search");
                }
            }

            return View(loginViewModel);
        }

        [HttpGet]
        [AuthorizationFilter("1","2","3")]
        public IActionResult Logout()
        {
            // Clear user from session
            authProvider.LogOff();

            // Redirect the user where you want them to go after logoff
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
       public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {

                // Register them as a new user (and set default role)
                // When a user registeres they need to be given a role. If you don't need anything special
                // just give them "User".
                if (authProvider.Register(registerViewModel.Email, registerViewModel.Password, 2) == false)
                {
                    return View(registerViewModel);
                }

                // Redirect the user where you want them to go after registering
                return RedirectToAction("Registered", "Account");
            }
            
            return View(registerViewModel);
        }

        [AuthorizationFilter]
        [HttpGet]
        public IActionResult Registered()
        {
            return View();
        }
        
        [HttpGet]
        [AuthorizationFilter("2")]  //user
        public IActionResult AddMyEvent(int id)
        {
            User user = authProvider.GetCurrentUser();
            List<Event> allMyEvents = eventSqlDal.GetUserEvents(user);

            foreach (Event eventItem in allMyEvents)
            {
                if (7 == eventItem.EventID)  //TODO remove
                {
                    return View("EventDetail");
                }
                else
                {
                    bool result = userEventSqlDal.AddMyEvent(user.UserID, eventItem);


                }

            }

           
            return View();//TODO remove this

        }

        public IActionResult MySchedule()
        {
            User user = authProvider.GetCurrentUser();
            List<Event> myEvents = eventSqlDal.GetUserEvents(user);
            return View(myEvents);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter("2")]  //user
        public IActionResult RemoveEvent(int userID, int eventID)
        {
            User user = authProvider.GetCurrentUser();
            Event eventItem = new Event();

            bool result = userEventSqlDal.RemoveMyEvent(user.UserID, eventItem.EventID);

            return View();//TODO remove

        }
    }


}//namespace   
        





