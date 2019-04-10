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
        private string connectionString;

        private const string SQL_GetAllEvents = "SELECT * FROM Event GROUP BY beginning ORDER BY ASC;";
        private const string SQL_GetEvent = "SELECT * FROM Event WHERE eventID = @eventID;";

        public EventSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
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
                   eventList.Add(MapToRowPodcast(reader));
                }
            }
            return eventList;
        }


       

        private Event MapToRowPodcast(SqlDataReader reader)
        {
            return new Event()
            {
                EventId = Convert.ToInt32(reader["eventID"]),
                PodcastId = Convert.ToInt32(reader["podcastID"]),
                VenueId = Convert.ToInt32(reader["venueID"]),
                Beginning = Convert.ToDateTime(reader["beginning"]),
                Ending = Convert.ToDateTime(reader["ending"]),
                Logo = Convert.ToString(reader["logo"]),
                Copy = Convert.ToString(reader["copy"]),
                PodcastURL = Convert.ToString(reader["podcastURL"]),
                TicketLevel = Convert.ToString(reader["ticketLevel"]),
                UpsaleCopy = Convert.ToString(reader["upsaleCopy"]),
                IsFinalized = Convert.ToBoolean(reader["isFinalized"])
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