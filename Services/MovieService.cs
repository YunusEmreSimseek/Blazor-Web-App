using System.Text.Json;
using BlazorApp1.Data;
using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Services
{
    public interface IMovieService
    {
        public Task<Movie> GetMovieAsync(string title);
        public Task<SearchMovies> GetMoviesAsync(string title);
        public Task<Movie> GetMovieByIdAsync(string imdbID);
    }
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://www.omdbapi.com/";
        private readonly string _apiKey = "2a62c73";

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Movie> GetMovieAsync(string title)
        {
            var url = $"{_baseUrl}?t={Uri.EscapeDataString(title)}&apikey={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) throw new Exception("The movie could not be retrieved from the OMDB service.");

            var content = await response.Content.ReadAsStringAsync();
            var movie = JsonSerializer.Deserialize<Movie>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return movie ?? new Movie();
        }

        public async Task<SearchMovies> GetMoviesAsync(string title)
        {
            var url = $"{_baseUrl}?s={Uri.EscapeDataString(title)}&apikey={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) throw new Exception("The movies could not be retrieved from the OMDB service.");

            var content = await response.Content.ReadAsStringAsync();
            var movies = JsonSerializer.Deserialize<SearchMovies>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return movies ?? new SearchMovies();
        }

        public async Task<Movie> GetMovieByIdAsync(string imdbID)
        {
            var url = $"{_baseUrl}?i={Uri.EscapeDataString(imdbID)}&apikey={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) throw new Exception("The movie could not be retrieved from the OMDB service.");

            var content = await response.Content.ReadAsStringAsync();
            var movie = JsonSerializer.Deserialize<Movie>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return movie ?? new Movie();
        }
    }
}