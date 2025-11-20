using CardOperationsService.Application.Cards.Queries.GetAllowedCardActions;
using CardOperationsService.Contracts.CardActions;
using CardOperationsService.Domain.Entities;
using CardOperationsService.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CardOperationsService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardActionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CardActionsController> _logger;

        public CardActionsController(IMediator mediator, ILogger<CardActionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("allowed")]
        public async Task<IActionResult> GetAllowedActions([FromBody] GetAllowedActionsRequest request)
        {
            if (!Enum.IsDefined(typeof(CardType), request.CardType))
                return BadRequest($"Invalid card type. Valid values: {string.Join(", ", Enum.GetNames<CardType>())}");

            if (!Enum.IsDefined(typeof(CardStatus), request.CardStatus))
                return BadRequest($"Invalid card status. Valid values: {string.Join(", ", Enum.GetNames<CardStatus>())}");

            try
            {
                var card = new CardDetails(
                    CardNumber: "",
                    CardType: request.CardType,
                    CardStatus: request.CardStatus,
                    IsPinSet: request.IsPinSet
                );

                var query = new GetAllowedCardActionsQuery(card);
                var result = await _mediator.Send(query);

                return Ok(new { allowedActions = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving allowed actions");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}
