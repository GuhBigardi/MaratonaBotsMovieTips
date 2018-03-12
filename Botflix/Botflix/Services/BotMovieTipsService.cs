using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Botflix.Model;
using Newtonsoft.Json;

namespace Botflix.Services
{
    public class BotMovieTipsService : IBotMovieTipsService
    {
        private HttpClient httpClient;
        private string URL;
        private Token token;

        public async Task ConfigureAuthentication()
        {

            URL = ConfigurationManager.AppSettings.Get("BmtUrl");

            using (httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respToken = await httpClient.PostAsync(
                    URL + "/api/Login", new StringContent(
                        JsonConvert.SerializeObject(new
                        {
                            UserID = ConfigurationManager.AppSettings.Get("BmtUser"),
                            AccessKey = ConfigurationManager.AppSettings.Get("BmtSubscriptionKey")
                        }), Encoding.UTF8, "application/json")).ConfigureAwait(false);

                string conteudo = await respToken.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (respToken.StatusCode == HttpStatusCode.OK)
                {
                    token = JsonConvert.DeserializeObject<Token>(conteudo);

                }
            }
        }

        public async Task<List<FavoriteMedia>> GetFavoriteMedias(string idUser)
        {
            httpClient = new HttpClient();
            if (token.Authenticated)
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.AccessToken);
            }
            var endpoint = $"/api/AddFavorites/?idUser={idUser}";
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
            httpClient = new HttpClient();
            if (token.Authenticated)
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.AccessToken);
            else
                throw new HttpRequestException("Token de acesso a BMT API inválido");
            try
            {

            var endpoint = $"/api/AddFavorites";
                HttpResponseMessage response = await httpClient.PostAsJsonAsync($"{URL}{endpoint}", favoriteMedia).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

    }
}