﻿using System;
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
        private const string SQL_GetEvent = "SELECT * FROM Event JOIN podcast ON Event.podcastID = Podcast.podcastID Join Venue ON Event.venueID = Venue.venueID WHERE eventID = @eventID;";
        private const string SQL_SaveEvent = "INSERT INTO Event (beginning, ending, logo, copy,  ticketLevel, upsaleCopy, isFinalized, name) VALUES (@beginning, @ending, @logo, @copy,  @ticketLevel, @upsaleCopy, @isFinalized, @name);";

        
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
                cmd.Parameters.AddWithValue("@logo", eventItem.CoverPhoto);
                cmd.Parameters.AddWithValue("@copy", eventItem.DescriptionCopy);
                //cmd.Parameters.AddWithValue("@podcastURL", eventItem.PodcastURL);
                cmd.Parameters.AddWithValue("@ticketLevel", eventItem.TicketLevel);
                cmd.Parameters.AddWithValue("@upsaleCopy", eventItem.UpsaleCopy);
                cmd.Parameters.AddWithValue("@isFinalized", eventItem.IsFinalized);
                cmd.Parameters.AddWithValue("@name", eventItem.Name);
                
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
        

        private Event MapToRowEvent(SqlDataReader reader)
        {
            return new Event()
            {
                EventID = Convert.ToInt32(reader["eventID"]),
                VenueID = Convert.ToString(reader["displayName"]),
                Beginning = Convert.ToDateTime(reader["beginning"]),
                Ending = Convert.ToDateTime(reader["ending"]),
                CoverPhoto = Convert.ToString(reader["logo"]),
                DescriptionCopy = Convert.ToString(reader["copy"]),
                //PodcastURL = Convert.ToString(reader["podcastURL"]),
                TicketLevel = Convert.ToString(reader["ticketLevel"]),
                UpsaleCopy = Convert.ToString(reader["upsaleCopy"]),
                IsFinalized = Convert.ToBoolean(reader["isFinalized"]),
                Name = Convert.ToString(reader["name"]),
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
//        Name = Convert.ToString(reader["parkName"]), // parkName
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
