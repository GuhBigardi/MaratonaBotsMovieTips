using System;
using BotMovieTips.API.Domain;

namespace BotMovieTips.API.Models
{
    public class FavoriteMediaViewModel
    {
        public FavoriteMediaViewModel(FavoriteMedia favoriteMedia)
        {
            if (favoriteMedia != null)
            {
                IdUser = favoriteMedia.IdUser;
                IdMedia = favoriteMedia.IdMedia;
                TransationDate = favoriteMedia.TransationDate;
            }
        }

        public string IdUser { get; set; }
        public int IdMedia { get; set; }
        public DateTime TransationDate { get; set; }
    }
}
