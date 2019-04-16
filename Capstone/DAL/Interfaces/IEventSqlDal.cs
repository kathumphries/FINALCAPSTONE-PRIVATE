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
        bool UpdateEventDetails(Event eventItem);
        void RemoveEvent(int eventID);
        List<Event> GetUserEvents(User user);
        List<Event> Search(Event eventItem, User user);
        List<Event> GetFutureEventsByDay(Event eventItem, User user);
        List<Event> GetPastEvents(Event eventItem, User user);
        List<Event> GetFutureEvents(Event eventItem, User user);
    }
}
