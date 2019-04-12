using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL.Interfaces;
using Capstone.Models;
using Capstone.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Controllers
{
    public class PodcastController : Controller
    {
        private readonly IPodcastSqlDal podcastDal;
        private readonly IGenreSqlDal genreSqlDal;


        public PodcastController(IPodcastSqlDal podcastSqlDal, IGenreSqlDal genreSqlDal)
        {
            this.podcastDal = podcastSqlDal;
            this.genreSqlDal = genreSqlDal;
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
            ;
        }

        //// GET: Podcast/Create
        //public ActionResult Create()
        //{
        //    Podcast podcast = new Podcast();
        //    PodcastViewModel model = new PodcastViewModel()
        //    {


        //        GenreList = GetGenreList(),


        //    };

        //    return View(model);
        //}

        //// POST: Podcast/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(PodcastViewModel model)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    else
        //    {

        //        model.GenreList = GetGenreList();
        //        bool result = podcastDal.AddPodcast(model.Podcast);


        //        return RedirectToAction("Detail", new {id = model.Podcast.PodcastID});

        //    }
        //}

        // GET: Podcast/Edit/5
        public ActionResult Edit(int id)
        {
            PodcastViewModel model = new PodcastViewModel
            {
                Podcast = podcastDal.GetPodcast(id.ToString()),
            };

           
            model.GenreList = GetGenreList();
            

            return View(model);

        }

        //// POST: Podcast/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id)
        //{
        //    PodcastViewModel model = new PodcastViewModel()
        //    {


        //    }
        //    {
        //        return View(model);
        //    }
        //    else
        //    {

        //        model.GenreList = GetGenreList();
        //        bool result = podcastDal.AddPodcast(model.Podcast);


        //        return RedirectToAction("Detail", new {id = model.Podcast.PodcastID});

        //    }


        //    //// GET: Podcast/Delete/5
        //    //public ActionResult Delete(int id)
        //    //{
        //    //    return View();
        //    //}

        //    //// POST: Podcast/Delete/5
        //    //[HttpPost]
        //    //[ValidateAntiForgeryToken]
        //    //public ActionResult Delete(int id, IFormCollection collection)
        //    //{
        //    //    try
        //    //    {
        //    //        // TODO: Add delete logic here

        //    //        return RedirectToAction(nameof(Index));
        //    //    }
        //    //    catch
        //    //    {
        //    //        return View();
        //    //    }
        //    //}


        //    public List<SelectListItem> GetGenreList()
        //    {
        //        List<Genre> genreList = genreSqlDal.GetAllGenres();

        //        List<SelectListItem> selectListGenre = new List<SelectListItem>();

        //        foreach (Genre item in genreList)
        //        {
        //            selectListGenre.Add(new SelectListItem(item.GenreName, item.GenreID.ToString()));
        //        }

        //        return selectListGenre;
        //    }
        //}

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

    }
}