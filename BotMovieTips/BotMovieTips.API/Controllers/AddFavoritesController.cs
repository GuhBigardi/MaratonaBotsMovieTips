using BotMovieTips.API.Domain;
using BotMovieTips.API.Models;
using BotMovieTips.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BotMovieTips.API.Controllers
{
    [Route("api/[controller]")]
    public class AddFavoritesController : Controller
    {
        private FavoriteMediaRepository favoriteMediaRepository;
        public AddFavoritesController()
        {
            if (favoriteMediaRepository == null)
                favoriteMediaRepository = new FavoriteMediaRepository();
        }

        [HttpGet]
        public List<FavoriteMediaViewModel> Get(string idUser)
        {
            var favoriteMedias = favoriteMediaRepository.GetById(idUser);

            List<FavoriteMediaViewModel> favoriteMediasViewModel = new List<FavoriteMediaViewModel>();
            foreach (var favoriteMedia in favoriteMedias)
            {
                if (favoriteMedia != null)
                    favoriteMediasViewModel.Add(new FavoriteMediaViewModel(favoriteMedia));
            }

            return favoriteMediasViewModel;
        }

        [HttpPost]
        public void Post([FromBody]FavoriteMediaViewModel favoriteMediaViewModel)
        {
            var favoriteMedia = new FavoriteMedia()
            {
                IdUser = favoriteMediaViewModel.IdUser,
                IdMedia = favoriteMediaViewModel.IdMedia,
                TransationDate = favoriteMediaViewModel.TransationDate
            };

            favoriteMediaRepository.Add(favoriteMedia);
        }

    }
}