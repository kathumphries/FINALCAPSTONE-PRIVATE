using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone.Test
{
    [TestClass()]
    public class EventSqlDalTests
    {
        TransactionScope transaction;

        private readonly string connectionString = @"Data Source=.\\sqlexpress; Initial Catalog=PodfestMidwestDB;Integrated Security = true;";
        private int podcastEventCount = 0;
        private int podcastID;
        private int userID;
        private int genreID;
        private int venueID;

        /*Before adding the Event, get the number of known Events*/

        [TestInitialize]
        public void Initialize()
        {
            transaction = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                connection.Open();

                /*Get the number of existing Events*/

                command = new SqlCommand("SELECT COUNT(*) FROM Event;", connection);
                podcastEventCount = (int)command.ExecuteScalar();

                /*Create new Event for testing*/
                command = new SqlCommand("INSERT INTO Podcast(hosting, url, title, description, originalRelease, runTime, releaseFreqency, averageLength, numOfEpisodes, numOfDownloads, measurementPlatform, demographics, affiliations, broadcastCity, broadcastState, inOhio, isSponsored)" +
                    "VALUES ('GoDaddy.com', 'petCount.com', 'Pet Count = 17', 'Animals are better than people', '1 / 14 / 2019 09: 00 am', 3, 'daily', 7, 70, 0, 'Job Offers', 'Eclectic', 'Rev1 Ventures', 'VIP', 'Columbus', 'Ohio', 1, 1); SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                podcastID = (int)command.ExecuteScalar();

                command = new SqlCommand("INSERT INTO Venue(displayName, roomName, buildingName, address1, address2, city, state, zipCode, phoneNumber, additionalInfo, parkingInfo, isVisible)" +
                    "VALUES ('', 'Main', 'Southern Theatre', '21 E. Main Street', '', 'Columbus', 'Ohio', '43215', '(614) 469-0939', '', 'Columbus Commons Parking Garage & Ohio Statehouse Underground Parking Garage', 1); SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                venueID = (int)command.ExecuteScalar();

                command = new SqlCommand("INSERT INTO Event(podcastID, venueID, beginning, ending, coverPhoto, descriptionCopy, ticketLevel, upsaleCopy, isFinalized, eventName)" +
                    "VALUES (@podcastID, @venueID, '11 / 15 / 2016 03: 39 pm', '4 / 10 / 2019 10: 39 pm', 'catCount@johnfoulton.com', 'Enter Copy Here', 'dogCount@johnfoulton.com', 'VIP', 'All the cool kids are doing it!', 1, 'Pet Count = 17'); SELECT CAST(SCOPE_IDENTITY() as int);", connection);

                command.Parameters.AddWithValue("@podcastID", podcastID);
                command.Parameters.AddWithValue("@venueID", venueID);

                int eventID = (int)command.ExecuteScalar();                         
                
            }
        }

        // Cleanup runs after every single test

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose(); //<-- disposing the transaction without committing it means it will get rolled back
        }


        [TestMethod()]
        public void GetAllEventsTest()
        {
            //Arrange
            EventSqlDal eventSqlDal = new EventSqlDal(connectionString);

            //Act
            List<Event> eventItems = eventSqlDal.GetAllEvents();

            //Assert
            Assert.IsNotNull(eventItems);
            Assert.AreEqual(eventItems.Count + 1, eventItems.Count);

            bool found = false;

            foreach (Event eventItem in eventItems)
            {
                if (eventItem.Podcast.URL == "catCount@johnfoulton.com")
                {
                    found = true;
                }

                Assert.AreEqual(true, found, "Event 'Pet Count = 17' not found in test");
                  
            }
        }
           
    }

}

                //Create User
                //command = new SqlCommand("INSERT INTO User(name, email, isAdmin, isProducer)" +
                //    "VALUES ('John Fulton', 'john@techelevator.com', 1, 1); SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                //userID = (int)command.ExecuteScalar();
