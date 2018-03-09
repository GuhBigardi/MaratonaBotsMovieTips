using BotMovieTips.API.Domain;
using System.Collections.Generic;

namespace BotMovieTips.API.Repositories
{
    public interface IFavoriteMediaRepository
    {
        List<FavoriteMedia> GetById(string userId);
        void Add(FavoriteMedia favoriteMedia);
    }
}
