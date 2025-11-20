using CardOperationsService.Application.Cards.Queries.GetAllowedCardActions;

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
        public async Task<IActionResult> GetAllowedActions([FromBody] CardDetails card)
        {
            if (card == null)
                return BadRequest("Card details are required");

            if (string.IsNullOrWhiteSpace(card.CardNumber))
                return BadRequest("Card number is required");

            if (!Enum.IsDefined(typeof(CardType), card.CardType))
                return BadRequest($"Invalid card type. Valid values: {string.Join(", ", Enum.GetNames<CardType>())}");

            if (!Enum.IsDefined(typeof(CardStatus), card.CardStatus))
                return BadRequest($"Invalid card status. Valid values: {string.Join(", ", Enum.GetNames<CardStatus>())}");

            try
            {
                var query = new GetAllowedCardActionsQuery(card);
                var result = await _mediator.Send(query);
                return Ok(new { allowedActions = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving allowed actions for card {CardNumber}", card.CardNumber);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}
