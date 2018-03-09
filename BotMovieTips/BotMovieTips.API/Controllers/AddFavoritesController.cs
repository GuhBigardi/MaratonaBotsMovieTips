using BotMovieTips.API.Domain;
using BotMovieTips.API.Models;
using BotMovieTips.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BotMovieTips.API.Controllers
{
    public class AddFavoritesController : Controller
    {
        private FavoriteMediaRepository favoriteMediaRepository;
        public AddFavoritesController(FavoriteMediaRepository favoriteMediaRepository)
        {
            this.favoriteMediaRepository = favoriteMediaRepository;
        }

        [HttpGet]
        [Route("v1/AddFavorites/{idUser}")]
        public List<FavoriteMediaViewModel> Get(string idUser)
        {
            var favoriteMedias =  favoriteMediaRepository.GetById(idUser);

            List<FavoriteMediaViewModel> favoriteMediasViewModel = new List<FavoriteMediaViewModel>();
            foreach (var favoriteMedia in favoriteMedias)
            {
                favoriteMediasViewModel.Add(new FavoriteMediaViewModel(favoriteMedia));
            }

            return favoriteMediasViewModel;
        }

        [HttpPost]
        [Route("v1/AddFavorites")]
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