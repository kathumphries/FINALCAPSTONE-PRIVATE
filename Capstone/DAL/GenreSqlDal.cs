﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL.Interfaces;
using Capstone.Models;

namespace Capstone.DAL
{
    public class GenreSqlDal : IGenreSqlDal
    {
        private string connectionString;

        private const string SQL_GetAllGenres = "SELECT * FROM Genre WHERE isVisible = 1 ORDER BY genreID ASC;";
        private const string SQL_GetGenreDescription = "SELECT name FROM Genre WHERE  genreID = @genreID;";
        private const string SQL_GetGenre = "SELECT * FROM Genre WHERE genreID = @genreID;";
        private const string SQL_GetGenreEventID = "SELECT * FROM Event JOIN Podcast ON Event.podcastID = Podcast.podcastID JOIN Genre ON Podcast.genreID = Genre.genreID WHERE Event.eventID = @eventID;";

        public GenreSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Genre> GetAllGenres()
        {
            List<Genre> genreList = new List<Genre>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetAllGenres, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    genreList.Add(MapToRowGenreName(reader));

                }
            }

            return genreList;
        }

        private Genre MapToRowGenreName(SqlDataReader reader)
        {

            return new Genre()
            {
                GenreID = Convert.ToInt32(reader["genreID"]),
                GenreName = Convert.ToString(reader["name"]),
                IsVisible = Convert.ToBoolean(reader["isVisible"])
            };


        }

        public string GetGenreDescription(int genreID)
        {
            string genreDescription = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();



                SqlCommand command = new SqlCommand(SQL_GetGenreDescription, connection);

                command.Parameters.AddWithValue("@genreID", genreID);
                genreDescription = command.ExecuteScalar().ToString();


                return genreDescription;
            }
        }

        public Genre GetGenre(int genreID)
        {
            Genre genre = new Genre();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetGenre, connection);

                command.Parameters.AddWithValue("@genreID", genreID);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    genre = (MapToRowGenreName(reader));

                }



                return genre;

            }

        }

        public Genre GetGenreEventID(int eventID)
        {
            Genre genre = new Genre();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetGenreEventID, connection);

                command.Parameters.AddWithValue("@eventID", eventID);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    genre = (MapToRowGenreName(reader));

                }



                return genre;

            }

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
