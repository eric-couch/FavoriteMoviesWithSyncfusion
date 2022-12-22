using FavoriteMovies.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Syncfusion.Blazor.Grids;
using System.Data;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Notifications;

namespace FavoriteMovies.Client.Pages
{
    public partial class Search
    {
        [Inject]
        public HttpClient Http { get; set; } = new();
        private List<MovieSearchResultItem> OMDBMovies = new();
        private string searchTerm = String.Empty;
        private int? year = null;
        private int page = 1;
        private int totalItems = 0;
        private SfGrid<MovieSearchResultItem>? MoviesGrid;
        public SfPager Page { get; set; }
        public SfToast? ToastObj;
        public List<MovieSearchResultItem>? SelectedMovies { get; set; }
        public string selectedPoster = String.Empty;
        private string toastContent = String.Empty;
        private readonly string OMDBAPIKey = "86c39163";
        private readonly string OMDBAPIUrl = "https://www.omdbapi.com/?apikey=";

        private async Task SearchOMDB()
        {
            string yearSearch = year is null ? String.Empty : $"&y={year}";
            MovieSearchResult? movieResult = await Http.GetFromJsonAsync<MovieSearchResult>($"{OMDBAPIUrl}{OMDBAPIKey}&s={searchTerm}{yearSearch}&page={page}");
            if (movieResult is not null)
            {
                OMDBMovies = movieResult.Search.ToList();
                totalItems = movieResult.totalResults;
            }
        }

        public async Task PageClick(PagerItemClickEventArgs args)
        {
            page = args.CurrentPage;
            await SearchOMDB();
        }

        public async Task GetSelectedRecords(RowSelectEventArgs<MovieSearchResultItem> args)
        {
            SelectedMovies = await MoviesGrid.GetSelectedRecordsAsync();
            MovieSearchResultItem ItemSelected = args.Data;
            selectedPoster = ItemSelected.Poster;
        }

        public async Task ToolbarClickHandler(ClickEventArgs args)
        {
            switch (args.Item.Id)
            {
                case "GridMovieAdd":
                    if (SelectedMovies is not null)
                    {
                        foreach (var movie in SelectedMovies)
                        {
                            Movie newMovie = new() { imdbId = movie.imdbID };
                            var res = await Http.PostAsJsonAsync("api/add-movie", newMovie);
                            if (res.IsSuccessStatusCode)
                            {
                                toastContent = $"Added {movie.Title} to your favorites";
                                StateHasChanged();
                                await Task.Delay(100);
                                await ToastObj.ShowAsync();
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
