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
        const string SQL_GetVenue = "SELECT * FROM Venue WHERE venueID = @venueID";


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
                DisplayName = Convert.ToString(reader["venueName"]),
                Address1 = Convert.ToString(reader["address1"]),
                City = Convert.ToString(reader["city"]),
                State = Convert.ToString(reader["state"]),
                ZipCode = Convert.ToInt32(reader["zipcode"]),
                PhoneNumber = Convert.ToString(reader["phoneNumber"]),
                AdditionalInfo = Convert.ToString(reader["additionalInfo"]),
                ParkingInfo = Convert.ToString(reader["parkingInfo"]),
                IsVisible = Convert.ToBoolean(reader["isVisible"])
            };
        }


        public Venue GetVenue(string venueId)
        {
            Venue venue = new Venue();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetVenue, connection);
                command.Parameters.AddWithValue("@venueId", Convert.ToInt32(venueId));
                var reader = command.ExecuteReader();
                while (reader.Read())
                {

                    venue = MapToRowVenueName(reader);

                }
            }

            return venue;
        }


    }//class
}//namespace
