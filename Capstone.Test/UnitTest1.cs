using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;
using System.Data.SqlClient;




namespace Capstone.Test
{
    [TestClass()]
    public class EventSqlDalTests
    {
        TransactionScope transaction;

        private string connectionString = @"Data Source=.\\sqlexpress; Initial Catalog=PodfestMidwestDB;Integrated Security = true";

        /*Before adding the Event, get the number of known Events*/

        [TestInitialize]
        public void Initialize()
        {
            transaction = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                connection.Open();

                command = new SqlCommand("INSERT INTO Event VALUES (1, '11 / 15 / 2016 03: 39 pm', '4 / 10 / 2019 10: 39 pm', 1, 1, 'catCount@johnfoulton.com', 'Enter Copy Here', 'dogCount@johnfoulton.com', 'VIP', 'All the cool kids are doing it!', 1, 'The More Pets The Better');", connection);
                command.ExecuteNonQuery();

                command = new SqlCommand("INSERT INTO Podcast(hosting, url, title, description, originalRelease, runTime, releaseFreqency, averageLength, numOfEpisodes, numOfDownloads, measurementPlatform, demographics, affiliations, broadcastCity, broadcastState, inOhio, isSponsored)" +
                    "VALUES ('GoDaddy.com', 'petCount.com', 'Animals are better than people', 1 / 14 / 2019 09: 00 am', 3, 'daily', 7, 70, 0, 'Job Offers', 'Eclectic', 'Rev1 Ventures', 'VIP', 'Columbus', 'Ohio', 1, 1); SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                //podcastID = (int)command.ExecuteScalar();
                //userID = (int)command.ExecuteScalar();
                //genreID = (int)command.ExecuteScalar();

            }

        }
        // Cleanup runs after every single test
        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose(); //<-- disposing the transaction without committing it means it will get rolled back
        }

                          
        //[TestMethod()]
        //public void GetAllEventsTest()
        //{
        //    //Arrange
        //    EventSqlDal eventSqlDal = new EventSqlDal(connectionString);

        //    //Act
        //    List<Event> events = eventSqlDal.GetAllEvents();

        //    //Assert
        //    Assert.IsNotNull(events);
        //    Assert.AreEqual(events + 1, events.Count);

        //    bool found = false;

        //    foreach (Event event in events)
        //    {
        //        if (event.PodcastURL == "catCount@johnfoulton.com")
        //            found = true;
        //    }

        //    Assert.AreEqual(true, found, "Country ABC not found in test");
        //}
    }
}
