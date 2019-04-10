﻿using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAL.Interfaces
{
    interface IEventSqlDal
    {

        List<Event> GetAllEvents();
        bool AddEventDetail(Event eventItem);

    }
}
