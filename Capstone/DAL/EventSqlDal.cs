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

        private const string SQL_GetAllEvents = "SELECT * FROM Event GROUP BY beginning ORDER BY ASC;";

        private const string SQL_GetEvent = "SELECT * FROM Event JOIN Podcast ON Event.podcastID = Podcast.podcastID Join Venue ON Event.venueID = Venue.venueID WHERE eventID = @eventID;";

        //private const string SQL_AddEventDetail = "INSERT INTO Event (beginning, ending, coverPhoto, descriptionCopy,  ticketID, upsaleCopy, isFinalized, eventName) VALUES (@beginning, @ending, @coverPhoto, @descriptionCopy,  @ticketID, @upsaleCopy, @isFinalized, @eventName);";



        private const string SQL_SaveEvent = "INSERT INTO Event (beginning, ending, podcastID, venueID, coverPhoto, descriptionCopy, ticketID, upsaleCopy, isFinalized, eventName) " +
                                             " VALUES (@beginning, @ending, @podcastID, @venueID, @coverPhoto, @descriptionCopy,  @ticketID, @upsaleCopy, @isFinalized, @eventName);";










        private string SQL_GetEventsByTimeOfDay = "SELECT * FROM Event WHERE DATEPART(hh, [beginning]) >= 3 AND DATEPART(hh, [beginning]) <= 10 " +
            "Union SELECT * FROM Event WHERE DATEPART(hh, [beginning]) > 10 AND DATEPART(hh, [beginning]) <= 15 " +
            "Union SELECT * FROM Event WHERE DATEPART(hh, [beginning]) > 15 AND DATEPART(hh, [beginning]) <= 24 ORDER BY beginning ASC;";

        private const string SQL_GetEventsByGenre = "SELECT * FROM Event JOIN Podcast ON Event.podcastID = Podcast.podcastID JOIN Genre ON Podcast.genreID = Genre.genreID  WHERE genre.genreID = @genreID ORDER BY beginning ASC;";
        private const string SQL_GetEventsByTicket = "SELECT * FROM Event WHERE ticketID = @ticketID ORDER BY beginning ASC;";
        private const string SQL_GetEventsByLocation = "SELECT * FROM Event WHERE venueID = @locationID ORDER BY beginning ASC;";


        private const string SQL_UpdateEventDetails = "UPDATE event SET beginning=@beginning,ending=@ending,coverPhoto=@logo,descriptionCopy=@copy,ticketID=@ticketID,upsaleCopy=@upsaleCopy,isFinalized=@isFinalized,eventName=@eventName,podcastID=@podcastID,venueID=@venueID WHERE eventID = @eventID";

                                                                                                                                        
                                                                                                                                       
                                                                                                            
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




        public List<Event> GetEventsByTimeOfDay(bool morning, bool afternoon, bool evening)
        {
            List<Event> eventItem = new List<Event>();

            if (morning && afternoon && evening)
            {
                
            }
            else if (morning && afternoon && !evening)
            {
                SQL_GetEventsByTimeOfDay = "SELECT * FROM Event WHERE DATEPART(hh, [beginning]) >= 3 AND DATEPART(hh, [beginning]) <= 10 " +
                            "Union SELECT * FROM Event WHERE DATEPART(hh, [beginning]) > 10 AND DATEPART(hh, [beginning]) <= 15 " +
                            "ORDER BY beginning ASC;";
            }
            else if (morning && evening && !afternoon)
            {
                SQL_GetEventsByTimeOfDay = "SELECT * FROM Event WHERE DATEPART(hh, [beginning]) >= 3 AND DATEPART(hh, [beginning]) <= 10 " +
                            "Union SELECT * FROM Event WHERE DATEPART(hh, [beginning]) > 15 AND DATEPART(hh, [beginning]) <= 24 ORDER BY beginning ASC;";

            }
            else if (afternoon && evening && !morning)
            {
                SQL_GetEventsByTimeOfDay = "SELECT * FROM Event WHERE DATEPART(hh, [beginning]) > 10 AND DATEPART(hh, [beginning]) <= 15 " +
                    "Union SELECT * FROM Event WHERE DATEPART(hh, [beginning]) > 15 AND DATEPART(hh, [beginning]) <= 24 ORDER BY beginning ASC;";
                
            }
            else if (morning && !afternoon && !evening)
            {
                SQL_GetEventsByTimeOfDay = "SELECT * FROM Event WHERE DATEPART(hh, [beginning]) >= 3 AND DATEPART(hh, [beginning]) <= 10 ORDER BY beginning ASC;";
            }
            else if (afternoon && !morning && !evening) 
            {
                SQL_GetEventsByTimeOfDay = "SELECT * FROM Event WHERE DATEPART(hh, [beginning]) > 10 AND DATEPART(hh, [beginning]) <= 15 ORDER BY beginning ASC;";
            }
            else if (evening && !afternoon && !morning)
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

        public List<Event> GetEventsByLocation(int locatonID)
        {
            List<Event> eventItem = new List<Event>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetEventsByLocation, connection);

                command.Parameters.AddWithValue("@locationID", locatonID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    eventItem.Add(MapToRowEvent(reader));
                }
            }

            return eventItem;
        }

        public List<Event> GetEventsByGenre(int genreID)
        {
            List<Event> eventItem = new List<Event>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetEventsByGenre, connection);

                command.Parameters.AddWithValue("@genreID", genreID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    eventItem.Add(MapToRowEvent(reader));
                }
            }

            return eventItem;
        } 

        public List<Event> GetEventsByTicket(int ticketID)
        {
            List<Event> eventItem = new List<Event>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetEventsByTicket, connection);

                command.Parameters.AddWithValue("@ticketID", ticketID);

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
            

            private Event MapToRowEvent(SqlDataReader reader)
        {
            return new Event()
            {
                EventID = Convert.ToInt32(reader["eventID"]),
                VenueID = Convert.ToString(reader["VenueID"]),
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












//public Park GetParkDetail(string parkCode)
//{
//    Park park = new Park();
//    using (SqlConnection connection = new SqlConnection(connectionString))
//    {
//        connection.Open();

//        SqlCommand command = new SqlCommand(SQL_GetParkDetail, connection);
//        command.Parameters.AddWithValue("@parkCode", parkCode);
//        var reader = command.ExecuteReader();
//        while (reader.Read())
//        {

//            park = MaptToRowPark(reader);

//        }
//    }

//    return park;
//}

//public List<Park> GetParks()
//{
//    List<Park> parkList = new List<Park>();

//    using (SqlConnection connection = new SqlConnection(connectionString))
//    {
//        connection.Open();

//        SqlCommand command = new SqlCommand(SQL_GetParks, connection);
//        var reader = command.ExecuteReader();
//        while (reader.Read())
//        {

//            parkList.Add(MaptToRowPark(reader));

//        }
//    }
//    return parkList;
//}

//private Park MaptToRowPark(SqlDataReader reader)
//{
//    return new Park()
//    {
//        ParkCode = Convert.ToString(reader["parkCode"]),
//        EventName = Convert.ToString(reader["parkName"]), // parkName
//        State = Convert.ToString(reader["state"]), //state
//        Acreage = Convert.ToInt32(reader["acreage"]), //acreage
//        ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]), //elevationInFeet
//        MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]), //milesOfTrail
//        NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]), //numberOfCampsites
//        Climate = Convert.ToString(reader["climate"]), //climate
//        ParkDescription = Convert.ToString(reader["parkDescription"]), //parkDescription
//        YearFounded = Convert.ToInt32(reader["yearFounded"]), //yearFounded
//        AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]), //annualVisitorCount
//        Quote = Convert.ToString(reader["inspirationalQuote"]), //inspirationalQuote
//        QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]), //inspirationalQuoteSource
//        EntryFee = Convert.ToInt32(reader["entryFee"]), //entryFee
//        NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]), //numbeOfAnimalSpecies

//    };



//public void SaveSurvey(DailySurvey survey)
//{
//    using (SqlConnection connection = new SqlConnection(connectionString))
//    {
//        connection.Open();
//        SqlCommand cmd = new SqlCommand(SQL_SaveNewSurvey, connection);
//        cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
//        cmd.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
//        cmd.Parameters.AddWithValue("@state", survey.State);
//        cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

//        cmd.ExecuteNonQuery();
//    }

//}
