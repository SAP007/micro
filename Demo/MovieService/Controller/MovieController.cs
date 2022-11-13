using Microsoft.AspNetCore.Mvc;
using MovieService.Models;
using MovieService.Service;
using MovieService;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MovieService.Controller
{

    [Route("[controller]")]
    [Controller]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieService _movieService;

        public MovieController(ILogger<MovieController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public string MessageRecieved(string inMessage)
        {
            Console.WriteLine(" - Message Recieved");
            
            //var list = new List<Object>();
            var list = new List<string>();


            MovieMessage? movieMessage = JsonSerializer.Deserialize<MovieMessage>(inMessage);

            if (movieMessage.FunctionToExecute == "GetAllMovies")
            {
                list = _movieService.GetAllMovies();
                
            }
            else if (movieMessage.FunctionToExecute == "SearchMovies")
            {
                list = _movieService.SearchMovies(movieMessage.Columns.Item1, movieMessage.Columns.Item2);
            }

            return JsonSerializer.Serialize(list);

        }

    }
}

