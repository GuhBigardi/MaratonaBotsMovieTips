using BotMovieTips.API.Domain;
using BotMovieTips.API.Models;
using BotMovieTips.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BotMovieTips.API.Controllers
{
    [Route("api/[controller]")]
    public class AddFavoritesController : Controller
    {
        private IFavoriteMediaRepository favoriteMediaRepository;
        public AddFavoritesController(IFavoriteMediaRepository favoriteMediaRepository)
        {
            this.favoriteMediaRepository = favoriteMediaRepository;
        }

        [Authorize("Bearer")]
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

        [Authorize("Bearer")]
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