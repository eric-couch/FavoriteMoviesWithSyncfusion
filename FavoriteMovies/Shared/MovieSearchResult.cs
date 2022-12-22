using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoriteMovies.Shared
{
    public class MovieSearchResult
    {
        public MovieSearchResultItem[] Search { get; set; }
        public int totalResults { get; set; }
        public string Response { get; set; }
    }
}
