using FavoriteMovies.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace FavoriteMovies.Client.Pages
{
    public partial class Index
    {
        [Inject]
        public HttpClient Http { get; set; } = new();
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public UserDto User = null;
        public List<OMDBMovie> userFavoriteMovies { get; set; } = new();
        public OMDBMovie MovieDetails { get; set; }
        public bool isVisible = true;
        private readonly string OMDBAPIKey = "86c39163";
        private readonly string OMDBAPIUrl = "https://www.omdbapi.com/?apikey=";

        protected override async Task OnInitializedAsync()
        {
            var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            if (UserAuth is not null && UserAuth.IsAuthenticated)
            {
                User = await Http.GetFromJsonAsync<UserDto>("api/User/");
                if (User?.FavoriteMovies is not null)
                {
                    foreach (var movie in User.FavoriteMovies)
                    {
                        OMDBMovie omdbMovie = await Http.GetFromJsonAsync<OMDBMovie>($"{OMDBAPIUrl}{OMDBAPIKey}&i={movie.imdbId}");
                        if (omdbMovie is not null)
                        {
                            userFavoriteMovies.Add(omdbMovie);
                        }
                    }
                }
            }
            isVisible = false;
        }

        private async Task ShowMovieDetails(OMDBMovie movie)
        {
            MovieDetails = movie;
            await Task.CompletedTask;
        }
    }
    
}
