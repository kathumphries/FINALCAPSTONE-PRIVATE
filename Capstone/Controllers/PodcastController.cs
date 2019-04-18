using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Capstone.Models;
using Capstone.DAL.Interfaces;
using Capstone.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using Capstone.Providers.Auth;

namespace Capstone.Controllers
{
    [AuthorizationFilter("1")]
    public class PodcastController : Controller
    {
        private readonly IPodcastSqlDal podcastDal;
        private readonly IGenreSqlDal genreSqlDal;
      
        const string SessionName = "User_Auth";

        private readonly IAuthProvider authProvider;

        public PodcastController(IAuthProvider authProvider, IPodcastSqlDal podcastSqlDal, IGenreSqlDal genreSqlDal)
        {
            this.podcastDal = podcastSqlDal;
            this.genreSqlDal = genreSqlDal;
            this.authProvider = authProvider;
        }

        // GET: Podcast
        public ActionResult Index()
        {
            List<Podcast> podcastList = podcastDal.GetAllPodcasts();
            return View(podcastList);
        }

        // GET: Podcast/Details/5

        public ActionResult Detail(int id)
        {
            PodcastViewModel model = new PodcastViewModel()
            {
                Podcast = podcastDal.GetPodcast(id.ToString()),
                GenreList = GetGenreList()
            };

            return View(model);

        }

        //// GET: Podcast/Create

        public ActionResult Create()
        {
            PodcastViewModel model = new PodcastViewModel()
            {
                Podcast = new Podcast(),
                GenreList = GetGenreList()
            };

            return View(model);
        }

        // POST: Podcast/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PodcastViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.GenreList = GetGenreList();
                return View(model);
            }
            else
            {

                model.GenreList = GetGenreList();
                podcastDal.CreatePodcast(model.Podcast);

                LogChange(model.Podcast.PodcastID, "CREATE");
                return RedirectToAction("Index");

            }
        }

        // GET: Podcast/Edit/5

        public ActionResult Edit(int id)
        {
            PodcastViewModel model = new PodcastViewModel
            {
                Podcast = podcastDal.GetPodcast(id.ToString()),
                GenreList = GetGenreList()
            };
            return View(model);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PodcastViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.GenreList = GetGenreList();
                return View(model);
            }
            else
            {
                model.GenreList = GetGenreList();
                bool result = podcastDal.UpdatePodacast(model.Podcast);

            LogChange(id, "EDIT");

            return RedirectToAction("Detail", new { id });


            }
        }

        public List<SelectListItem> GetGenreList()
        {
            List<Genre> genreList = genreSqlDal.GetAllGenres();

            List<SelectListItem> selectListGenre = new List<SelectListItem>();

            foreach (Genre item in genreList)
            {
                selectListGenre.Add(new SelectListItem(item.GenreName, item.GenreID.ToString()));
            }

            return selectListGenre;
        }

    
        private void LogChange(int id, string action)
        {
            User user = authProvider.GetCurrentUser();
            DateTime timeChanged = DateTime.Now;
            Podcast podcast = podcastDal.GetPodcast(id.ToString());

            string eventDetail = user.Email + "," +
                                 user.Name + "," +
                                 action + "PODCAST, " +
                 "PodcastID:  " + podcast.PodcastID + "," +
                "UserID:  " + podcast.UserID + "," +
                "Hosting:  " + podcast.Hosting + "," +
                "URL:  " + podcast.URL + "," +
                "Title:  " + podcast.Title + "," +
                "Description:  " + podcast.Description + "," +
                "GenreID:  " + podcast.GenreID + "," +
                "OriginalRelease:  " + podcast.OriginalRelease + "," +
                "RunTime:  " + podcast.RunTime + "," +
                "ReleaseFrequency:  " + podcast.ReleaseFrequency + "," +
                "AverageLength:  " + podcast.AverageLength + "," +
                "MeasurementPlatform:  " + podcast.MeasurementPlatform + "," +
                "Demographic:  " + podcast.Demographic + "," +
                "Affiliations:  " + podcast.Affiliations + "," +
                "BroadcastCity:  " + podcast.BroadcastCity + "," +
                "BroadcastState:  " + podcast.BroadcastState + "," +
                "IsSponsored:  " + podcast.IsSponsored + "," +
                "Sponsor:  " + podcast.Sponsor;

            System.IO.File.AppendAllText(@"c:\pmlog\log.txt", (eventDetail + "\n"));

        }


    }
}