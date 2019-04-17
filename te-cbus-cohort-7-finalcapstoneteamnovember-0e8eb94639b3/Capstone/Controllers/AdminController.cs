using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Capstone.DAL.Interfaces;
using Capstone.Models.ViewModel;
using Capstone.Models.Account;
using Capstone.Providers.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; //needed for the SetString and GetString extension methods
using Capstone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;



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
        //private readonly ITicketSqlDal ticketSqlDal;



        public AdminController(IAuthProvider authProvider, IUserSqlDal userSqlDal)
            //IPodcastSqlDal podcastSqlDal, IEventSqlDal eventSqlDal,
            //IGenreSqlDal genreSqlDal, IVenueSqlDal venueSqlDal, ITicketSqlDal ticketSqlDal)
        {
            this.authProvider = authProvider;
            this.userSqlDal = userSqlDal;
            //this.podcastDal = podcastSqlDal;
            //this.eventSqlDal = eventSqlDal;
            //this.genreSqlDal = genreSqlDal;
            //this.venueSqlDal = venueSqlDal;
            //this.ticketSqlDal = ticketSqlDal;
        }

        //Roles in db: 
        //("1" - Admin, "2 - RegisterUser", "3 - Podcaster", "4 - Anonymous"

        //[AuthorizationFilter] // actions can be filtered to only those that are logged in




        [AuthorizationFilter("1")]  //<-- or filtered to only those that have a certain role
         [HttpGet]
         public IActionResult Index()
            {
                List<User> users = userSqlDal.GetAllUsersByRole();
                return View(users);
            }

//            [AuthorizationFilter("1")] 
//            [HttpPost]
//            if (ModelState.IsValid)
//           {
//                foreach (User user in users)
//                {
//                    var getCode = _context.TBMapBalances.Where(p => p.TbMapId == TbListId.TbMapId).FirstOrDefault();

//                    if (getCode != null)
//                    {
//                        getCode.TbMapId = TbListId.TbMapId;
//                    }

//                }
//// _context.Update(tbMapViewModel.TBMapBalances);
//                _context.SaveChanges();

//            }

//            return RedirectToAction("TbMapView");
//    }







    }
}