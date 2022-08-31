using Internship_2022.Application.Interfaces;
using Internship_2022.Application.Models.MessageDto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Internship_2022_august_be1.Controllers
{
    [ ApiController, Route("[controller]/api")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;
        private readonly ILogger<ListingController> logger;

        public MessageController(IMessageService messageService, ILogger<ListingController> logger)
        {
            this.messageService = messageService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("send")]
        public async Task<ActionResult> SendMessageAsync(MessageRequestDto message)
        {
            try
            {
                await messageService.SendMessageAsync(message);
                logger.LogInformation("Message sent");

                return Ok();
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{listingId}")]
        public async Task<ActionResult<List<Guid>>> GetMessagesAsync(Guid listingId)
        {
            try
            {
                var result = await messageService.GetMessagesAsync(listingId);
                logger.LogInformation("Messages retrived");

                return Ok(result);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{listingId}")]
        public async Task<ActionResult> DeleteMessageAsync(Guid listingId)
        {
            try
            {
                await messageService.DeleteMessageAsync(listingId);
                logger.LogInformation("Message deleted");

                return Ok();
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
        }
    }
}

