using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAL.Interfaces
{
    public interface IEventSqlDal
    {
        List<Event> GetAllEvents();
        bool SaveEvent(Event eventItem);
        Event GetEvent(int eventID);

    }
}
