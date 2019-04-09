using System;
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
        private string connectionString;

        private const string SQL_GetPodcasts = "SELECT * FROM Podcast;";
      
        public PodcastSqlDal(string connectionString)
        {
            this.connectionString = connectionString;

        }

        public List<Podcast> GetAllPodcasts()
        {
            List<Podcast> podcasts = new List<Podcast>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(SQL_GetPodcasts, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Podcast podcast = new Podcast();

                        podcast.PodcastId = Convert.ToInt32(reader["podcastID"]);
                        podcast.UserId = Convert.ToInt32(reader["userID"]);
                        podcast.Hosting = Convert.ToString(reader["hosting"]);
                        podcast.URL = Convert.ToString(reader["url"]);
                        podcast.Title = Convert.ToString(reader["title"]);
                        podcast.Description = Convert.ToString(reader["description"]);
                        podcast.GenreId = Convert.ToInt32(reader["genreID"]);
                        //podcast.SoundByte = Convert.ToString(reader["soundByte"]);
                        podcast.OriginalRelease = Convert.ToDateTime(reader["originalRelease"]);
                        podcast.RunTime = Convert.ToDouble(reader["runTime"]);
                        podcast.ReleaseFrequency = Convert.ToString(reader["releaseFrequency"]);
                        podcast.AverageLength = Convert.ToDouble(reader["averageLength"]);
                        podcast.NumOfEpisodes = Convert.ToInt32(reader["numOfEpisodes"]);
                        podcast.NumOfDownloads = Convert.ToInt32(reader["numOfDownloads"]);
                        podcast.MeasurementPlatform = Convert.ToString(reader["measurementPlatform"]);
                        podcast.Demographics = Convert.ToString(reader["demographics"]);
                        podcast.Affiliations = Convert.ToString(reader["affiliations"]);
                        podcast.broadcastCity = Convert.ToString(reader["broadcastCity"]);
                        podcast.broadcastState = Convert.ToString(reader["broadcastState"]);
                        podcast.InOhio = Convert.ToBoolean(reader["inOhio"]);
                        podcast.IsSponsored = Convert.ToBoolean(reader["isSponsored"]);

                        podcasts.Add(podcast);


                    }
                }
            }
            catch (Exception ex)
            {
                podcasts = new List<Podcast>();
                throw ex;
            }

            return podcasts;
        }
    }
}