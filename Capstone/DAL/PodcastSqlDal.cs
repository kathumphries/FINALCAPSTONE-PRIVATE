﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL.Interfaces;
using Capstone.Models;

namespace Capstone.DAL
{
    public class PodcastSqlDal : IPodcastSqlDal
    {
        private readonly string connectionString;

        private const string SQL_GetAllPodcasts = "SELECT * FROM Podcast ORDER BY title;";
        private const string SQL_GetPodcast = "SELECT * FROM Podcast WHERE podcastID = @podcastID;";


        public PodcastSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Podcast> GetAllPodcasts()
        {
            List<Podcast> podcastList = new List<Podcast>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetAllPodcasts, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                   podcastList.Add(MapToRowPodcast(reader));
                                        
                }
            }
            return podcastList;
        }

        public Podcast GetPodcast(string podcastID)
        {
            Podcast podcast = new Podcast();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(SQL_GetPodcast, connection);
                command.Parameters.AddWithValue("@podcastID", podcastID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    podcast = MapToRowPodcast(reader);
                }

            }
            return podcast;
        }

        private Podcast MapToRowPodcast(SqlDataReader reader)
        {
            return new Podcast()
            {
                PodcastID = Convert.ToInt32(reader["podcastID"]),
                UserID = Convert.ToInt32(reader["userID"]),
                Hosting = Convert.ToString(reader["hosting"]),
                URL = Convert.ToString(reader["url"]),
                Title = Convert.ToString(reader["title"]),
                Description = Convert.ToString(reader["description"]),
                GenreID = Convert.ToInt32(reader["genreID"]),
                //SoundByte = Convert.ToString(reader["soundByte"]),
                OriginalRelease = Convert.ToDateTime(reader["originalRelease"]),
                RunTime = Convert.ToString(reader["runTime"]),
                ReleaseFrequency = Convert.ToString(reader["releaseFrequency"]),
                AverageLength = Convert.ToString(reader["averageLength"]),
                EpisodeCount = Convert.ToInt32(reader["numOfEpisodes"]),
                DownloadCount = Convert.ToInt32(reader["numOfDownloads"]),
                MeasurementPlatform = Convert.ToString(reader["measurementPlatform"]),
                Demographic = Convert.ToString(reader["demographics"]),
                Affiliations = Convert.ToString(reader["affiliations"]),
                BroadcastCity = Convert.ToString(reader["broadcastCity"]),
                BroadcastState = Convert.ToString(reader["broadcastState"]),
                IsSponsored = Convert.ToBoolean(reader["isSponsored"])

            };


        }
    }
}


