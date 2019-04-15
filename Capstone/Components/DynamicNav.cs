using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL.Interfaces;
using Capstone.Models;
using Capstone.Providers.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Componets
{
    public class DynamicNav: ViewComponent
    {

        const string SessionName = "User_Auth";

        private readonly IAuthProvider authProvider;
        private readonly IUserSqlDal userSqlDal;

        public DynamicNav(IAuthProvider authProvider, IUserSqlDal userSqlDal)
           
        {
            this.authProvider = authProvider;
            this.userSqlDal = userSqlDal;
        }

        
        public IViewComponentResult Invoke()
        {
            User model = authProvider.GetCurrentUser();


            return View(model);

        }


    }
}


