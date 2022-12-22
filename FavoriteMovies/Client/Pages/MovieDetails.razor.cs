using FavoriteMovies.Shared;
using Microsoft.AspNetCore.Components;

namespace FavoriteMovies.Client.Pages
{
    public partial class MovieDetails
    {
        [Parameter]
        public OMDBMovie? Movie { get; set; }
    }
}
