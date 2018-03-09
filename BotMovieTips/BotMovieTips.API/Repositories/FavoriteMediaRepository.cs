using BotMovieTips.API.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BotMovieTips.API.Repositories
{
    public class FavoriteMediaRepository
    {
        public FavoriteMediaRepository()
        {
            if (FavoriteMoviesInstance == null)
                FavoriteMoviesInstance = new List<FavoriteMedia>();
        }
        public List<FavoriteMedia> FavoriteMoviesInstance { get; set; }
        public void Add(FavoriteMedia favoriteMedia)
        {
            FavoriteMoviesInstance.Add(favoriteMedia);
        }

        public List<FavoriteMedia> GetById(string idUser)
        {
            return FavoriteMoviesInstance.Where(x => x.IdUser == idUser).ToList();
        }
    }
}
