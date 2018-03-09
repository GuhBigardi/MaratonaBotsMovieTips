using System;

namespace BotMovieTips.API.Domain
{
    public class FavoriteMedia
    {
        public string IdUser { get; set; }
        public int IdMedia { get; set; }
        public DateTime TransationDate { get; set; }
    }
}
