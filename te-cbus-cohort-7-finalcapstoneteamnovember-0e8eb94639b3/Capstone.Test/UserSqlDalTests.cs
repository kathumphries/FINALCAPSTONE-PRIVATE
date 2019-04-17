using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone.Test
{
    class UserSqlDalTests
    {
        //Create User
        //command = new SqlCommand("INSERT INTO User(name, email, isAdmin, isProducer)" +
        //    "VALUES ('John Fulton', 'john@techelevator.com', 1, 1); SELECT CAST(SCOPE_IDENTITY() as int);", connection);
        //userID = (int)command.ExecuteScalar();

    }
}
