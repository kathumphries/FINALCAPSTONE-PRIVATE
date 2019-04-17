using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAL.Interfaces
{
    public interface IGenreSqlDal
    {
        List<Genre> GetAllGenres();
        string GetGenreDescription(int genreID);
        Genre GetGenre(int genreID);
        Genre GetGenreEventID(int eventID);
    }
}
