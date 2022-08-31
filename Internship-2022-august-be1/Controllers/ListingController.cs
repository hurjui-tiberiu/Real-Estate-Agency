using Internship_2022.Application.Interfaces;
using Internship_2022.Application.Models.ListingDtos;
using Internship_2022.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Internship_2022_august_be1.Controllers
{
    [ApiController, Route("[controller]/api")]
    public class ListingController : ControllerBase
    {

        private readonly IListingService listingService;
        private readonly ILogger<ListingController> logger;

        public ListingController(IListingService listingService, ILogger<ListingController> logger)
        {
            this.listingService = listingService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("listing")]
        public async Task<ActionResult<IReadOnlyList<GetListingByIdRequestDto>>> GetListingsAsync()
        {
            try
            {
                var result = await listingService.GetListingAsync();
                logger.LogInformation("Listing retrived");

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateListing(CreateListingRequestDto listing, Guid userId)
        {
            try
            {
                await listingService.CreateListingAsync(listing, userId);
                logger.LogInformation("Listing created");

                return Ok();
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }

        }

        [HttpPut]
        [Route("{id}")]
        public async Task UpdateListing(Guid id, UpdateListingRequestDto listing)
        {
            try
            {
                var result = await listingService.GetListingByIdAsync(id);
                if (result == null)
                {
                    NotFound();
                }
                await listingService.UpdateListingAsync(id, listing);
                logger.LogInformation("Listing updated");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                BadRequest();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GetListingByIdRequestDto>> GetListingByIDAsync(Guid id)
        {
           // try
         //   {
                var result = await listingService.GetListingByIdAsync(id);
                logger.LogInformation("Listing retrived");

                return Ok(result);
          //  }
         //   catch (Exception ex)
         //   {
           //     return NotFound();
         //   }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Listing>> DeleteListing(Guid id)
        {
          //  try
         //   {
                await listingService.DeleteListingAsync(id);
                logger.LogInformation("Listing Deleted");

                return Ok();
         //   }
        //    catch (Exception ex)
       //     {
       //         return NotFound();
       //     }
        }

        [HttpGet, Route("/api/listing/{userId}")]
        public async Task<ActionResult<GetListingRequestDto>> GetListingsByUserIdAsync(Guid userId)
        {
            try
            {
                var listings = await listingService.GetListingsByUserIdAsync(userId);
                return Ok(listings);
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
        }
    }
}
