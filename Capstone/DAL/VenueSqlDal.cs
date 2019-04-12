using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL.Interfaces;
using Capstone.Models;

namespace Capstone.DAL
{
    public class VenueSqlDal : IVenueSqlDal
    {
        private readonly string connectionString;
        const string SQL_GetAllVenues = "SELECT * FROM Venue";
        //const string SQL_SaveReview = "";

        public VenueSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Venue> GetAllVenues()
        {
            List<Venue> venueList = new List<Venue>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetAllVenues, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    venueList.Add(MapToRowVenueName(reader));

                }
            }

            return venueList;
        }

        private Venue MapToRowVenueName(SqlDataReader reader)
        {

            return new Venue()
            {
                VenueID = Convert.ToInt32(reader["venueID"]),
                DisplayName = Convert.ToString(reader["displayName"]),
                RoomName = Convert.ToString(reader["roomName"]),
                BuildingName = Convert.ToString(reader["buildingName"]),
                Address1 = Convert.ToString(reader["address1"]),
                Address2 = Convert.ToString(reader["address2"]),
                City = Convert.ToString(reader["city"]),
                State = Convert.ToString(reader["state"]),
                ZipCode = Convert.ToInt32(reader["zipcode"]),
                PhoneNumber = Convert.ToString(reader["phoneNumber"]),
                AdditionalInfo = Convert.ToString(reader["additionalInfo"]),
                ParkingInfo = Convert.ToString(reader["parkingInfo"]),
                IsVisible = Convert.ToBoolean(reader["isVisible"])
            };
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


        
    }
}
