using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Capstone.DAL.Interfaces;
using Capstone.Models.ViewModel;
using Capstone.Models.Account;
using Capstone.Providers.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; //needed for the SetString and GetString extension methods
using Capstone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;


namespace Capstone.Controllers
{
    public class AdminController : Controller
    {
        const string SessionName = "User_Auth";


        private readonly IAuthProvider authProvider;
        private readonly IUserSqlDal userSqlDal;
        //private readonly IPodcastSqlDal podcastDal;
        //private readonly IEventSqlDal eventSqlDal;
        //private readonly IGenreSqlDal genreSqlDal;
        //private readonly IVenueSqlDal venueSqlDal;
        private readonly ITicketSqlDal ticketSqlDal;



        public AdminController(IAuthProvider authProvider, IUserSqlDal userSqlDal, ITicketSqlDal ticketSqlDal)
        //IPodcastSqlDal podcastSqlDal, IEventSqlDal eventSqlDal,
        //IGenreSqlDal genreSqlDal, IVenueSqlDal venueSqlDal, ITicketSqlDal ticketSqlDal , ITicketSqlDal ticketSqlDal)

        {
            this.authProvider = authProvider;
            this.userSqlDal = userSqlDal;
            //this.podcastDal = podcastSqlDal;
            //this.eventSqlDal = eventSqlDal;
            //this.genreSqlDal = genreSqlDal;
            //this.venueSqlDal = venueSqlDal;
             this.ticketSqlDal = ticketSqlDal;
        }

        //Roles in db: 
        //("1" - Admin, "2 - RegisterUser", "3 - Podcaster", "4 - Anonymous"

        //[AuthorizationFilter] // actions can be filtered to only those that are logged in




        [AuthorizationFilter("1")]  //<-- or filtered to only those that have a certain role
        [HttpGet]
        public IActionResult Index()
        {
            List<User> users = userSqlDal.GetAllUsersByRole();
            users.ForEach(user =>
            {
                if (!String.IsNullOrEmpty(user.TicketLevel))
                {
                    user.Ticket = ticketSqlDal.GetTicket(user.TicketLevel);

                }
            });
            


            //model.UserRoleID = userSqlDal.GetUserByID(id).Role;

            return View(users);
        }


        [HttpGet]
       // [AuthorizationFilter("1")]
        public IActionResult EditRole(int id)
       {

           UserRoleAdminViewModel model = new UserRoleAdminViewModel();
            model.UserID = id;
            
            model.UserName = userSqlDal.GetUserByID(id).Name;
            model.UserEmail = userSqlDal.GetUserByID(id).Email;
            model.UserRoleID = userSqlDal.GetUserByID(id).Role;
            return View(model);

        }

        [AuthorizationFilter("1")] //admin only
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRole(UserRoleAdminViewModel model, int id)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {

                bool result = userSqlDal.UpdateUserRole(model);

                return RedirectToAction("Index", "Admin");


            }


        }


        [HttpGet]
          public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       [AuthorizationFilter("1")]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
            // Redirect the user where you want them to go after registering
                return RedirectToAction("Register", "Admin");
            }

            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult RegisterCompleted()
        {
            return View();
        }


        //create update delete for venue
        // //create update delete for venue
        //creat ticketLevel update add visible...?
        //create tags crud?
        //genre editor


        public List<SelectListItem> GetTicketList()
        {
            List<Ticket> ticketList = ticketSqlDal.GetAllTickets();

            List<SelectListItem> selectListTickets = new List<SelectListItem>();

            foreach (Ticket level in ticketList)
            {
                selectListTickets.Add(new SelectListItem(level.TicketType, level.TicketID.ToString()));
            }

            return selectListTickets;
        }



    }

}


