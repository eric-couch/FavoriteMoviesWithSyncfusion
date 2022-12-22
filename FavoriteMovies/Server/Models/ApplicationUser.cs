using FavoriteMovies.Shared;
using Microsoft.AspNetCore.Identity;

namespace FavoriteMovies.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public List<Movie> FavoriteMovies { get; set; } = new List<Movie>();
    }
}