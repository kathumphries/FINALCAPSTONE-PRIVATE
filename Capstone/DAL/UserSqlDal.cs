using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL.Interfaces;
using Capstone.Models;
using Capstone.Models.Account;

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
        private const string SQL_GetUserByID = "SELECT * FROM [PodfestMidwestDB].[dbo].[User] WHERE userID = @userID;";
        private const string SQL_CreateUser = "INSERT INTO [PodfestMidwestDB].[dbo].[User] (email, password, salt, role) VALUES (@email, @password, @salt, @role);";  // add extra requirements...
        private const string SQL_GetAllUsers = "SELECT * FROM [PodfestMidwestDB].[dbo].[User] ORDER BY role, email;";
        private const string SQL_DeleteUser = "DELETE FROM user WHERE userID = @userID;";
        private const string SQL_UpdateUser ="UPDATE user SET password = @password, salt = @salt, role = @role WHERE userID = @userID;";

        private const string SQL_GetAllUsersByRole =
            "SELECT* FROM[PodfestMidwestDB].[dbo].[User] ORDER BY role, email;";
        private const string SQL_UpdateUserRole = "UPDATE [PodfestMidwestDB].[dbo].[User] SET role=@role WHERE userID = @userID";



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


        public User GetUserByID(int id)
        {
            User user = new User();
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(SQL_GetUserByID, connection);
                    command.Parameters.AddWithValue("@userID",id);

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
             User user = new User()
            {
                UserID = Convert.ToInt32(reader["userID"]),
                Email = Convert.ToString(reader["email"]).ToLower(), //email
                PhoneNumber = Convert.ToString(reader["phoneNumber"]), //phoneNumber
                TicketLevel = Convert.ToString(reader["ticketLevel"]), //ticketLevel
                Password = Convert.ToString(reader["password"]), //password
                Role = Convert.ToInt32(reader["role"]), //role
                Salt  = Convert.ToString(reader["salt"]), //salt
                //Name = (String.IsNullOrEmpty(Convert.ToString(reader["name"])))? "No Name Provided" :
                Name = Convert.ToString(reader["name"])//name
            };


             return user;
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


        public List<User> GetAllUsersByRole()
        {
            List<User> users = new List<User>();


           
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(SQL_GetAllUsersByRole, connection);
                 

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        users.Add(MaptToRowUser(reader));

                    }

                    return users;
                }
            }
            catch (SqlException ex)
            {
                string exception = ex.ToString();
                users = null;
            }

            return users;
        }



        public bool UpdateUserRole(UserRoleAdminViewModel model)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_UpdateUserRole, connection);

                command.Parameters.AddWithValue("@userID", model.UserID);
                command.Parameters.AddWithValue("@role",model.UserRoleID);
          

                rowsAffected = command.ExecuteNonQuery();

            }

            if (rowsAffected == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }//class
}//namespace














