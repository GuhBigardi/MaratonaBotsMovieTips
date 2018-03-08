using Botflix.Model;
using System.Collections.Generic;

namespace Botflix.Services
{
    interface IBotMovieTipsService
    {
        List<FavoriteMedia> GetFavoriteMedias(string idUser);
        void SendFavoriteMedia(FavoriteMedia favoriteMedia);
    }
}
