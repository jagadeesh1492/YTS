using System.Collections.Generic;

namespace YTS.Mobile
{
    public class Data
    {
        public int MovieCount { get; set; }
        public int Limit { get; set; }
        public int PageNumber { get; set; }
        public List<Movie> Movies { get; set; }
    }
}