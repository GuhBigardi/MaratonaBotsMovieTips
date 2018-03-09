using System;
using System.Collections.Generic;
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

        public async Task<MediaSearch> GetMediaByName(string name, Category mediaType)
        {
            name = name.Replace(" ", "+");
            var endpoint = $"/search/multi/?api_key={subscriptionKey}&{language}&query={name}";
            HttpResponseMessage result;
            MediaSearch media = null;
            try
            {
                result = await httpClient.GetAsync($"{URL}{endpoint}");
                if (result.StatusCode != HttpStatusCode.OK)
                    throw new HttpRequestException();

                var resultString = await result.Content.ReadAsStringAsync();
                var mediaSearch = JsonConvert.DeserializeObject<MediaSearchResult>(resultString);
                if (mediaType != Category.anyway)
                    media = mediaSearch.results.Where(x => x.media_type == mediaType.ToString()).FirstOrDefault();
                else
                    media = mediaSearch.results.FirstOrDefault();

                return media;
            }
            catch (Exception ex)
            {
                throw ex;
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
                return movie;
            }
            catch (Exception ex)
            {
                var resultString = MovieMock.Content;
                var movie = JsonConvert.DeserializeObject<Movie>(resultString);

                return movie;
            }
        }
        public async Task<List<Media>> GetRecommendation(int id)
        {
            var endpoint = $"/movie/{id}/recommendations?page=1&api_key={subscriptionKey}&{language}";
            HttpResponseMessage result;
            try
            {
                result = await httpClient.GetAsync($"{URL}{endpoint}");
                if (result.StatusCode != HttpStatusCode.OK)
                    throw new HttpRequestException();

                var resultString = await result.Content.ReadAsStringAsync();
                var recommendationResult = JsonConvert.DeserializeObject<RecommendationResult>(resultString);
                return recommendationResult.results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}