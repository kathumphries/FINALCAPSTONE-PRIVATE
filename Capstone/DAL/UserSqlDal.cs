using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL.Interfaces;
using Capstone.Models;

namespace Capstone.DAL
{
    public class UserSqlDal : IUserSqlDal
    {
        private readonly string connectionString;

        public UserSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private const string SQL_GetCurrentUser = "SELECT * FROM [PodfestMidwestDB].[dbo].[User] WHERE email = @email;";
        private const string SQL_CreateUser = "INSERT INTO [PodfestMidwestDB].[dbo].[User] (email, password, salt, role) VALUES (@email, @password, @salt, @role);";  // add extra requirements...
        private const string SQL_GetAllUsers = "SELECT * FROM [PodfestMidwestDB].[dbo].[User] ORDER BY role, email;";
        private const string SQL_DeleteUser = "DELETE FROM user WHERE userID = @userID;";
        private const string SQL_UpdateUser ="UPDATE users SET password = @password, salt = @salt, role = @role WHERE userID = @userID;";

       
        public bool CreateUser(User user)
        {
            bool result = false;

           
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(SQL_CreateUser, connection);

                        command.Parameters.AddWithValue("@email", user.Email.ToLower());
                        command.Parameters.AddWithValue("@password", user.Password);
                        command.Parameters.AddWithValue("@salt", user.Salt);
                        command.Parameters.AddWithValue("@role", user.Role);

                        result = (command.ExecuteNonQuery() > 0) ? true : false;

                   
                    }
                }

                 catch (SqlException ex)
                 {
                     string exception = ex.ToString();
                     result = false;
                 }

            return result;
          
        }

       
        public void DeleteUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(SQL_DeleteUser, connection);
                   command.Parameters.AddWithValue("@userID", user.UserID);
                    command.ExecuteNonQuery();
                    return;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

    
        public User GetUser(string email)
        {
            User user = null;


            try
            {
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(SQL_GetCurrentUser, connection);
                    command.Parameters.AddWithValue("@email", email.ToLower());


                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        user = MaptToRowUser(reader);

                    }

                    return user;
                }
            }
            catch (SqlException ex)
            {
                string exception = ex.ToString();
                user = null;
            }

            return user;
        }

        
        public void UpdateUser(User user)
        {
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(SQL_UpdateUser, connection);
                    command.Parameters.AddWithValue("@password", user.Password);
                    command.Parameters.AddWithValue("@salt", user.Salt);
                    command.Parameters.AddWithValue("@role", user.Role);
                    command.Parameters.AddWithValue("@userID", user.UserID);
                    command.ExecuteNonQuery();

                    return;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        private User MaptToRowUser(SqlDataReader reader)
        {
            return new User()
            {
                UserID = Convert.ToInt32(reader["userID"]),
                Name = Convert.ToString(reader["name"]),//name
                Email = Convert.ToString(reader["email"]).ToLower(), //email
                PhoneNumber = Convert.ToString(reader["phoneNumber"]), //phoneNumber
                TicketLevel = Convert.ToString(reader["ticketLevel"]), //ticketLevel
                Password = Convert.ToString(reader["password"]), //password
                Role = Convert.ToInt32(reader["role"]), //role
                Salt  = Convert.ToString(reader["salt"]) //salt
            };
        }



        public bool DuplicateUser(User user)
        {
            return GetUser(user.Email.ToLower()) != null;
        }


        public List<User> GetAllUsers()
        {
            List<User> userList = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetAllUsers, connection);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {

                    userList.Add(MaptToRowUser(reader));

                }

                return userList;
            }
        }

       




    }//class
}//namespace


















//public User GetCurrentUser(string email, string password)
//        {
//           User user = new User();
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();

//                SqlCommand command = new SqlCommand(SQL_GetCurrentUser, connection);
//                command.Parameters.AddWithValue("@email", email);
                

//                var reader = command.ExecuteReader();
//                while (reader.Read())
//                {

//                    user = MaptToRowUser(reader);

//                }
//            }

//            return user;
//        }



