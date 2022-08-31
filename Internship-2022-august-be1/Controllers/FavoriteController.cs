using Internship_2022.Application.Interfaces;
using Internship_2022.Application.Models.ListingDtos;
using Internship_2022.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Internship_2022_august_be1.Controllers
{
    [ApiController, Route("[controller]/api")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService favoriteService;
        private readonly ILogger<FavoriteController> logger;

        public FavoriteController(IFavoriteService favoriteService, ILogger<FavoriteController> logger)
        {
            this.favoriteService = favoriteService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("{UserId}")]
        public async Task<ActionResult<IReadOnlyList<List<Guid>>>> GetFavoritesAsync()
        {
            try
            {
                Guid userId = new Guid(RouteData.Values["UserId"].ToString());
                var result = await favoriteService.GetListingFavoriteByUserId(userId);
                if (result.Count == 0)
                {
                    return NotFound();
                }
                logger.LogInformation("Favorites retrived succesfully");

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return NotFound();
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult> DeleteFavoritesAsync([FromQuery] Guid userId, Guid listingId)
        {
            try
            {
                await favoriteService.DeleteFavorite(userId, listingId);
                logger.LogInformation("Favorite deleted succesfully");

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("{UserId}")]
        public async Task<ActionResult> AddFavorite(Guid listingId)
        {
            try
            {
                Guid userId = new Guid(RouteData.Values["UserId"].ToString());
               var check= await favoriteService.AddFavorite(userId, listingId);
                if(check==false)
                {
                    logger.LogInformation("Favorite is already added.");
                    return BadRequest("Favorite is already added.");
                }
                logger.LogInformation("Favorite added succesfully");

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpDelete("{favoriteId}")]
        public async Task<IActionResult> DeleteListingFromFavorites(Guid favoriteId)
        {
            try
            {
                await favoriteService.DeleteById(favoriteId);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return NotFound();
            }
        }
    }
}
