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
        List<Event> GetEventsByTicket(Event ticketEvent);
        List<Event> GetEventsByLocation(Event venueEvent);
        List<Event> GetEventsByGenre(Event genreEvent);
        List<Event> GetEventsByTimeOfDay(string timeOfDayString);
        List<Event> GetUserEvents(User user);
    }
}
