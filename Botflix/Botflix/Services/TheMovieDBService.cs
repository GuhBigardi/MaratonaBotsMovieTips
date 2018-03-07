using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Botflix.Mocks;
using Botflix.Model;
using Newtonsoft.Json;

namespace Botflix.Services
{
    public class TheMovieDBService
    {
        private readonly HttpClient httpClient;
        private readonly string URL;
        private string subscriptionKey;
        private string language;

        public TheMovieDBService()
        {
            httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(1500)
            };
            subscriptionKey = ConfigurationManager.AppSettings.Get("MovieDBServiceKey");
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            URL = "https://api.themoviedb.org/3";
            language = "language=pt-BR";

        }

        public async Task<Movie> GetMovieByName(string name)
        {
            name = name.Replace(" ", "+");
            var endpoint = $"/search/movie/?api_key={subscriptionKey}&{language}&query={name}";
            HttpResponseMessage result;
            try
            {
                result = await httpClient.GetAsync($"{URL}{endpoint}");
                if (result.StatusCode != HttpStatusCode.OK)
                    throw new HttpRequestException();

                var resultString = await result.Content.ReadAsStringAsync();
                var movieSearch = JsonConvert.DeserializeObject<MovieSearchResult>(resultString);

                var movie = await GetMovieById(movieSearch.results.FirstOrDefault().id);

                return movie;
            }
            catch (Exception ex)
            {
                var resultString = MovieMock.Content;
                var movie = JsonConvert.DeserializeObject<Movie>(resultString);

                return movie;
            }
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var endpoint = $"/movie/{id}?api_key={subscriptionKey}&{language}";
            HttpResponseMessage result;
            try
            {
                result = await httpClient.GetAsync($"{URL}{endpoint}");
                if (result.StatusCode != HttpStatusCode.OK)
                    throw new HttpRequestException();

                var resultString = await result.Content.ReadAsStringAsync();
                var movie = JsonConvert.DeserializeObject<Movie>(resultString);
                movie.image = await GetMovieImage(id);
                return movie;
            }
            catch (Exception ex)
            {
                var resultString = MovieMock.Content;
                var movie = JsonConvert.DeserializeObject<Movie>(resultString);

                return movie;
            }
        }

        public async Task<string> GetMovieImage(int id)
        {
            var endpoint = $"/movie/{id}/images?api_key={subscriptionKey}&{language}";
            HttpResponseMessage result;
            try
            {
                result = await httpClient.GetAsync($"{URL}{endpoint}");
                if (result.StatusCode != HttpStatusCode.OK)
                    throw new HttpRequestException();

                var resultString = await result.Content.ReadAsStringAsync();
                var movie = JsonConvert.DeserializeObject<MovieImages>(resultString);

                return "";
            }
            catch (Exception ex)
            {
                var resultString = MovieMock.Content;
                var movie = JsonConvert.DeserializeObject<Movie>(resultString);

                return "";
            }
        }
    }
}