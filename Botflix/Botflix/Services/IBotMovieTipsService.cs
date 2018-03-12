using Botflix.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Botflix.Services
{
    interface IBotMovieTipsService
    {
        Task<List<FavoriteMedia>> GetFavoriteMedias(string idUser);
        Task<bool> SendFavoriteMedia(FavoriteMedia favoriteMedia);
        Task ConfigureAuthentication();
    }
}
