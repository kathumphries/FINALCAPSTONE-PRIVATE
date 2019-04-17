using Capstone.DAL.Interfaces;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class TicketSqlDal : ITicketSqlDal
    {
        private readonly string connectionString;

        private const string SQL_GetAllTickets = "SELECT * FROM TicketLevel;";
        private const string SQL_GetTicket = " Select * FROM TicketLevel WHERE ticketID = @ticketID;";

        public TicketSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Ticket> GetAllTickets()
        {
            List<Ticket> ticketList = new List<Ticket>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetAllTickets, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ticketList.Add(MapToRowTicket(reader));
                }
            }
            return ticketList;
        }


        public Ticket GetTicket(string ticketLevel)
        {
            Ticket ticket = new Ticket();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetTicket, connection);
                command.Parameters.AddWithValue("@ticketID", Convert.ToInt32(ticketLevel));
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ticket = (MapToRowTicket(reader));
                }
            }

            return ticket;
        }



        private Ticket MapToRowTicket(SqlDataReader reader)
        {
            return new Ticket()
            {
                TicketID = Convert.ToInt32(reader["ticketID"]),
                TicketType = Convert.ToString(reader["ticketType"]),
                IsVisible = Convert.ToBoolean(reader["isVisible"]),           
            };
        }

    }
}
