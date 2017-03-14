using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTS.Mobile.Helpers;
using YTS.Mobile.JsonModel;
using YTS.Mobile.Utilities;

namespace YTS.Mobile.Service
{
    public class MoviesService
    {
        private  JsonService JsonService { get; set; }
        

        public MoviesService()
        {
            JsonService=new JsonService();
        }
        public async Task<MoviesList>  GetDefaultMovies()
        {
            var movies=await JsonService.GetAsync<MoviesList>(Constants.ListMovies);
            return movies;
        }
        public async Task<MoviesList> GetMoviesByParameter(Dictionary<string, object> parameters)
        {
            var movies = await JsonService.GetAsync<MoviesList>(Constants.ListMovies, parameters);
            return movies;
        }
    }
}
