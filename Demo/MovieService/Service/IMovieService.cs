using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Service
{

    public interface IMovieService

    {
        public List<string> GetAllMovies();
        //public List<Object> GetAllMovies();

        public List<string> SearchMovies(string item1, string item2);
    }
}
