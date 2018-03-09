using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Botflix.Model;
using Newtonsoft.Json;

namespace Botflix.Services
{
    public class BotMovieTipsService : IBotMovieTipsService
    {
        private readonly HttpClient httpClient;
        private readonly string URL;
        private string subscriptionKey;

        public BotMovieTipsService()
        {
            httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(1500)
            };
            subscriptionKey = ConfigurationManager.AppSettings.Get("MovieDBServiceKey");
            //httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            URL = "http://localhost:63966";

        }

        public async Task<List<FavoriteMedia>> GetFavoriteMedias(string idUser)
        {
            var endpoint = $"/v1/AddFavorites/?idUser={idUser}";
            HttpResponseMessage result;
            try
            {
                result = await httpClient.GetAsync($"{URL}{endpoint}");
                if (result.StatusCode != HttpStatusCode.OK)
                    throw new HttpRequestException();

                var resultString = await result.Content.ReadAsStringAsync();
                var favoriteMedias = JsonConvert.DeserializeObject<List<FavoriteMedia>>(resultString);
                return favoriteMedias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SendFavoriteMedia(FavoriteMedia favoriteMedia)
        {
            var endpoint = $"/v1/AddFavorites";
            HttpResponseMessage response = await httpClient.PostAsJsonAsync($"{URL}{endpoint}", favoriteMedia).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
                return true;

            return false;

        }
    }
}