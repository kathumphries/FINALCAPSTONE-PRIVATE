using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL.Interfaces;
using Capstone.Models;

namespace Capstone.DAL
{
    public class EventSqlDal : IEventSqlDal
    {
        private readonly string connectionString;

        private const string SQL_GetAllEvents ="SELECT  eventID, beginning,ending ,podcastID ,venueID ,coverPhoto,descriptionCopy,ticketID,upsaleCopy,isFinalized,eventName " +
            "FROM Event  GROUP BY  CAST(event.beginning AS DATE), " +
            "eventID, beginning,ending ,podcastID ,venueID ,coverPhoto,descriptionCopy,ticketID,upsaleCopy,isFinalized,eventName  " +
            "Order by CAST(event.beginning AS DATE) ASC ;";

        private const string SQL_GetEvent = "SELECT * FROM Event JOIN Podcast ON Event.podcastID = Podcast.podcastID Join Venue ON Event.venueID = Venue.venueID WHERE eventID = @eventID;";

        //private const string SQL_AddEventDetail = "INSERT INTO Event (beginning, ending, coverPhoto, descriptionCopy,  ticketID, upsaleCopy, isFinalized, eventName) VALUES (@beginning, @ending, @coverPhoto, @descriptionCopy,  @ticketID, @upsaleCopy, @isFinalized, @eventName);";


        private const string SQL_GetUserEvents = "Select *FROM Event JOIN User_Event ON User_Event.eventID = Event.eventID WHERE Event.isFinalized = 1 AND User_Event.userID = @userID ORDER BY  beginning;";
        
        private const string SQL_SaveEvent = "INSERT INTO Event (beginning, ending, podcastID, venueID, coverPhoto, descriptionCopy, ticketID, upsaleCopy, isFinalized, eventName) " +
                                             " VALUES (@beginning, @ending, @podcastID, @venueID, @coverPhoto, @descriptionCopy,  @ticketID, @upsaleCopy, @isFinalized, @eventName);";
        
        private string SQL_GetEventsByTimeOfDay = "SELECT * FROM Event WHERE DATEPART(hh, [beginning]) >= 3 AND DATEPART(hh, [beginning]) <= 10 " +
            "Union SELECT * FROM Event WHERE DATEPART(hh, [beginning]) > 10 AND DATEPART(hh, [beginning]) <= 15 " +
            "Union SELECT * FROM Event WHERE DATEPART(hh, [beginning]) > 15 AND DATEPART(hh, [beginning]) <= 24 ORDER BY beginning ASC;";

        private const string SQL_GetEventsByGenre = "SELECT * FROM Event JOIN Podcast ON Event.podcastID = Podcast.podcastID JOIN Genre ON Podcast.genreID = Genre.genreID  WHERE Podcast.genreID = @genreID ORDER BY beginning ASC;";
        private const string SQL_GetEventsByTicket = "SELECT * FROM Event WHERE ticketID = @ticketID ORDER BY beginning ASC;";
        private const string SQL_GetEventsByLocation = "SELECT * FROM Event WHERE venueID = @locationID ORDER BY beginning ASC;";


        private const string SQL_UpdateEventDetails = "UPDATE event SET beginning=@beginning,ending=@ending,coverPhoto=@logo,descriptionCopy=@copy,ticketID=@ticketID,upsaleCopy=@upsaleCopy,isFinalized=@isFinalized,eventName=@eventName,podcastID=@podcastID,venueID=@venueID WHERE eventID = @eventID";

        private const string SQL_RemoveEvent = "  Delete from event where eventID = @eventID";                                                                                                                                 
        private const string SQL_GetEventsByDay = "SELECT * FROM Event WHERE DATEPART(dd, [beginning]) = @day ORDER BY beginning ASC;";

        public EventSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool SaveEvent(Event eventItem)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(SQL_SaveEvent, connection);

                cmd.Parameters.AddWithValue("@beginning", eventItem.Beginning);
                cmd.Parameters.AddWithValue("@ending", eventItem.Ending);
                cmd.Parameters.AddWithValue("@podcastID", eventItem.PodcastID);
                cmd.Parameters.AddWithValue("@venueID", eventItem.VenueID);
                cmd.Parameters.AddWithValue("@coverPhoto", eventItem.CoverPhoto);
                cmd.Parameters.AddWithValue("@descriptionCopy", eventItem.DescriptionCopy);
                cmd.Parameters.AddWithValue("@ticketID", eventItem.TicketLevel);
                cmd.Parameters.AddWithValue("@upsaleCopy", eventItem.UpsaleCopy);
                cmd.Parameters.AddWithValue("@isFinalized", eventItem.IsFinalized);
                cmd.Parameters.AddWithValue("@eventName", eventItem.EventName);
                
                count = cmd.ExecuteNonQuery();
            }

            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Event> GetAllEvents()
        {
            List<Event> eventList = new List<Event>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetAllEvents, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    eventList.Add(MapToRowEvent(reader));
                }
            }
            return eventList;
        }

        public Event GetEvent(int eventID)
        {
            Event eventItem = new Event();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetEvent, connection);
                command.Parameters.AddWithValue("@eventID", eventID);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    eventItem = MapToRowEvent(reader);
                }
            }

            return eventItem;
        }

        public void RemoveEvent(int eventID)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(SQL_RemoveEvent, connection))
                {
                    command.Parameters.AddWithValue("@eventID", eventID);
                    command.ExecuteNonQuery();
                }


            }

           
        }


        public List<Event> GetEventsByDay(int day)
        {
            List<Event> eventItem = new List<Event>();

            if (day == 0)
            {
                return eventItem;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetEventsByDay, connection);
                command.Parameters.AddWithValue("@day", day);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    eventItem.Add(MapToRowEvent(reader));
                }
            }

            return eventItem;
        }

        public List<Event> GetEventsByTimeOfDay(string timeOfDayString)
        {
            List<Event> eventItem = new List<Event>();

            if (timeOfDayString == null)
            {
                return eventItem;
            }

            int timeOfDay = Convert.ToInt32(timeOfDayString);
           

            if (timeOfDay == 1)
            {
                SQL_GetEventsByTimeOfDay = "SELECT * FROM Event WHERE DATEPART(hh, [beginning]) >= 3 AND DATEPART(hh, [beginning]) <= 10 ORDER BY beginning ASC;";
            }
            else if (timeOfDay == 2)
            {
                SQL_GetEventsByTimeOfDay = "SELECT * FROM Event WHERE DATEPART(hh, [beginning]) > 10 AND DATEPART(hh, [beginning]) <= 15 ORDER BY beginning ASC;";
            }
            else if (timeOfDay == 3)
            {
                SQL_GetEventsByTimeOfDay = "SELECT * FROM Event WHERE DATEPART(hh, [beginning]) > 15 AND DATEPART(hh, [beginning]) <= 24 ORDER BY beginning ASC;";
            }         
            else
            {
                return eventItem;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetEventsByTimeOfDay, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    eventItem.Add(MapToRowEvent(reader));
                }
            }

            return eventItem;
        }

        public List<Event> GetEventsByLocation(Event venueEvent)
        {
            List<Event> eventItem = new List<Event>();

            if (venueEvent.VenueID == null)
            {
                return eventItem;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetEventsByLocation, connection);

                command.Parameters.AddWithValue("@locationID", venueEvent.VenueID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    eventItem.Add(MapToRowEvent(reader));
                }
            }

            return eventItem;
        }


        
        public List<Event> GetEventsByGenre(Event genreEvent)
        {
            List<Event> eventItem = new List<Event>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetEventsByGenre, connection);

                command.Parameters.AddWithValue("@genreID", genreEvent.Podcast.GenreID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    eventItem.Add(MapToRowEvent(reader));
                }
            }

            return eventItem;
        } 

        public List<Event> GetEventsByTicket(Event ticketEvent)
        {
            List<Event> eventItem = new List<Event>();

            if (ticketEvent.TicketLevel == null)
            {
                return eventItem;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetEventsByTicket, connection);

                command.Parameters.AddWithValue("@ticketID", ticketEvent.TicketLevel);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    eventItem.Add(MapToRowEvent(reader));
                }
            }

            return eventItem;
        }

        public bool UpdateEventDetails(Event eventItem)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_UpdateEventDetails, connection);
                  
                command.Parameters.AddWithValue("@eventID", eventItem.EventID);
                command.Parameters.AddWithValue("@beginning", eventItem.Beginning);
                command.Parameters.AddWithValue("@ending", eventItem.Ending);
                command.Parameters.AddWithValue("@logo", eventItem.CoverPhoto);
                command.Parameters.AddWithValue("@copy", eventItem.DescriptionCopy);
                command.Parameters.AddWithValue("@ticketID", eventItem.TicketLevel);
                command.Parameters.AddWithValue("@upsaleCopy", eventItem.UpsaleCopy);
                command.Parameters.AddWithValue("@isFinalized", eventItem.IsFinalized);
                command.Parameters.AddWithValue("@eventName", eventItem.EventName);
                command.Parameters.AddWithValue("@podcastID", Convert.ToInt32(eventItem.PodcastID));
                command.Parameters.AddWithValue("@venueID", Convert.ToInt32(eventItem.VenueID));

            rowsAffected = command.ExecuteNonQuery();
                
            }

            if (rowsAffected == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public List<Event> GetUserEvents(User user)
        {
            List<Event> eventList = new List<Event>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetUserEvents, connection);
                command.Parameters.AddWithValue("@userID", user.UserID);
                
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    eventList.Add(MapToRowEvent(reader));
                }


                return eventList;
            }
        }

        private Event MapToRowEvent(SqlDataReader reader)
        {
            return new Event()
            {
                EventID = Convert.ToInt32(reader["eventID"]),
                VenueID = Convert.ToString(reader["venueID"]),
                Beginning = Convert.ToDateTime(reader["beginning"]),
                Ending = Convert.ToDateTime(reader["ending"]),
                CoverPhoto = Convert.ToString(reader["coverPhoto"]),
                DescriptionCopy = Convert.ToString(reader["descriptionCopy"]),
                //PodcastURL = Convert.ToString(reader["podcastURL"]),
                TicketLevel = Convert.ToString(reader["ticketID"]),
                UpsaleCopy = Convert.ToString(reader["upsaleCopy"]),
                IsFinalized = Convert.ToBoolean(reader["isFinalized"]),
                EventName = Convert.ToString(reader["eventName"]),
                //Podcast = Convert.ToString(reader["title"]),
                PodcastID = Convert.ToString(reader["podcastID"])
            };
        }


       
        

    }
}



