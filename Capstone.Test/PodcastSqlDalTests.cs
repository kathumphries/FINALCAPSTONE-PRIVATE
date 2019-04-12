using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone.Test
{
    class PodcastSqlDalTests
    {
        TransactionScope transaction;

        private readonly string connectionString = @"Data Source=.\\sqlexpress; Initial Catalog=PodfestMidwestDB;Integrated Security = true;";
        private int podcastID;
        private int userID;
        private int genreID;
        private int venueID;
        private int eventID;




    }
}
