using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoriteMovies.Shared
{
    public class UserDto
    {
        public string Id { get; set; } = String.Empty;
        public string UserName { get; set; } = String.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Movie> FavoriteMovies { get; set; } = new List<Movie>();
    }
}
