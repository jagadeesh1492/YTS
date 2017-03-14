using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTS.Mobile.Utilities
{
    public  class Constants
    {
        public static readonly string ApiBaseUrl = "https://yts.ag/api/v2/";

        public static readonly string ListMovies = $"{ApiBaseUrl}list_movies.json";
    }
}
