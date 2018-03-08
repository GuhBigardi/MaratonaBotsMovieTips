using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BotMovieTips.API.Controllers
{
    [Produces("application/json")]
    [Route("api/AddFavorites")]
    public class AddFavoritesController : Controller
    {

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

    }
}