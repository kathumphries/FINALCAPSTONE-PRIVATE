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
        private readonly string connectionString;

        private const string SQL_GetAllPodcasts = "SELECT * FROM Podcast ORDER BY title;";
        private const string SQL_GetPodcast = "SELECT * FROM Podcast WHERE podcastID = @podcastID;";
        private const string SQL_UpdatePodcast = "UPDATE podcast SET userID=@userID,hosting=@hosting,url=@url,title=@title,description=@description,genreID=@genreID,originalRelease=@originalRelease,datetime=@datetime,runTime=@runTime,releaseFrequency=@releaseFrequency,averageLength=@averageLength,episodeCount=@episodeCount,downloadCount=@downloadCount,measurementPlatform=@measurementPlatform,demographic=@demographic,affiliations=@affiliations,broadcastCity=@broadcastCity,broadcastState=@broadcastState,inOhio=@inOhio,isSponsored=@isSponsored,sponsor=@sponsor WHERE podcastID = @podcastID;";

        public PodcastSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }



        public bool UpdatePodacast(Podcast model)
        {
            bool updateSuccesful = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(SQL_UpdatePodcast, connection);
                    command.Parameters.AddWithValue("@podcastID", model.PodcastID);
                    command.Parameters.AddWithValue("@userID",model.UserID);
                    command.Parameters.AddWithValue("@hosting",model.Hosting);
                    command.Parameters.AddWithValue("@url",model.URL);
                    command.Parameters.AddWithValue("@title",model.Title);
                    command.Parameters.AddWithValue("@description",model.Description);
                    command.Parameters.AddWithValue("@genreID",model.GenreID);
                    command.Parameters.AddWithValue("@originalRelease",model.OriginalRelease);
                    command.Parameters.AddWithValue("@runTime",model.RunTime);
                    command.Parameters.AddWithValue("@releaseFrequency",model.ReleaseFrequency);
                    command.Parameters.AddWithValue("@averageLength",model.AverageLength);
                    command.Parameters.AddWithValue("@episodeCount",model.EpisodeCount);
                    command.Parameters.AddWithValue("@downloadCount",model.DownloadCount);
                    command.Parameters.AddWithValue("@measurementPlatform",model.MeasurementPlatform);
                    command.Parameters.AddWithValue("@demographic",model.Demographic);
                    command.Parameters.AddWithValue("@affiliations",model.Affiliations);
                    command.Parameters.AddWithValue("@broadcastCity",model.BroadcastCity);
                    command.Parameters.AddWithValue("@broadcastState",model.BroadcastState);
                    command.Parameters.AddWithValue("@inOhio",model.InOhio);
                    command.Parameters.AddWithValue("@isSponsored",model.IsSponsored);
                    command.Parameters.AddWithValue("@sponsor",model.Sponsor);


                    updateSuccesful = (command.ExecuteNonQuery() > 0) ? true : false;


                }
            }

            catch (SqlException ex)
            {
                string exception = ex.ToString();
                updateSuccesful = false;
            }

            return updateSuccesful;
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


<<<<<<< HEAD
            podcast = new Podcast()
            {
                PodcastID = Convert.ToInt32(reader["podcastID"]),
                UserID = Convert.ToInt32(reader["userID"]),
                Hosting = Convert.ToString(reader["hosting"]),           
                Title = Convert.ToString(reader["title"]),
                Description = Convert.ToString(reader["description"]),
                GenreID = Convert.ToInt32(reader["genreID"]),
                OriginalRelease = Convert.ToDateTime(reader["originalRelease"]),
                RunTime = Convert.ToString(reader["runTime"]),
                ReleaseFrequency = Convert.ToString(reader["releaseFrequency"]),
                AverageLength = Convert.ToString(reader["averageLength"]),
                MeasurementPlatform = Convert.ToString(reader["measurementPlatform"]),
                Demographic = Convert.ToString(reader["demographic"]),
                Affiliations = Convert.ToString(reader["affiliations"]),
                BroadcastCity = Convert.ToString(reader["broadcastCity"]),
                BroadcastState = Convert.ToString(reader["broadcastState"]),
                IsSponsored = Convert.ToBoolean(reader["isSponsored"])

            };

            int colIndex = reader.GetOrdinal("url");

            if (reader.IsDBNull(colIndex))
            {
               podcast.URL = "";
            }
            else
            {
               podcast.URL = Convert.ToString(reader["url"]);
            }

            colIndex = reader.GetOrdinal("episodeCount");

            if(reader.IsDBNull(colIndex))
            {
               podcast.EpisodeCount = 0;
            }
            else
            {   
               podcast.EpisodeCount = Convert.ToInt32(reader["episodeCount"]);
            }
=======
public bool AddPodcast(Podcast podcast)
{
    return true;
}


private Podcast MapToRowPodcast(SqlDataReader reader)
{
    Podcast podcast = new Podcast();

    podcast = new Podcast()
    {
        PodcastID = Convert.ToInt32(reader["podcastID"]),
        UserID = Convert.ToInt32(reader["userID"]),
        Hosting = Convert.ToString(reader["hosting"]),
        URL = Convert.ToString(reader["url"]),
        Title = Convert.ToString(reader["title"]),
        Description = Convert.ToString(reader["description"]),
        GenreID = Convert.ToInt32(reader["genreID"]),
        OriginalRelease = Convert.ToDateTime(reader["originalRelease"]),
        RunTime = Convert.ToString(reader["runTime"]),
        ReleaseFrequency = Convert.ToString(reader["releaseFrequency"]),
        AverageLength = Convert.ToString(reader["averageLength"]),
        MeasurementPlatform = Convert.ToString(reader["measurementPlatform"]),
        Demographic = Convert.ToString(reader["demographic"]),
        Affiliations = Convert.ToString(reader["affiliations"]),
        BroadcastCity = Convert.ToString(reader["broadcastCity"]),
        BroadcastState = Convert.ToString(reader["broadcastState"]),
        IsSponsored = Convert.ToBoolean(reader["isSponsored"]),
        Sponsor = Convert.ToString(reader["sponsor"])

            };

    int colIndex = reader.GetOrdinal("episodeCount");

    if (reader.IsDBNull(colIndex))
    {
        podcast.EpisodeCount = 0;
    }
    else
    {
        podcast.EpisodeCount = Convert.ToInt32(reader["episodeCount"]);
    }
>>>>>>> 31a90af6756a0eb664896bda421bef4f216039e1

    colIndex = reader.GetOrdinal("downloadCount");

    if (reader.IsDBNull(colIndex))
    {
        podcast.DownloadCount = 0;
    }
    else
    {
        podcast.DownloadCount = Convert.ToInt32(reader["downloadCount"]);
    }

    return podcast;

}
    }
}


