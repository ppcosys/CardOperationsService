using CardOperationsService.Application.Cards.Queries.GetCardDetails;
using CardOperationsService.Application.Cards.Queries.GetUserCards;
using CardOperationsService.Contracts.CardDetails;
using CardOperationsService.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace CardOperationsService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CardDetailsController> _logger;

        public CardDetailsController(IMediator mediator, ILogger<CardDetailsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{userId}/{cardNumber}")]
        public async Task<ActionResult<CardDetailsResponse>> GetCardDetails(string userId, string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(userId) || userId.Length > 50)
                return BadRequest("User ID is required and must be less than 50 characters");

            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length > 20)
                return BadRequest("Card number is required and must be less than 20 characters");

            try
            {
                var result = await _mediator.Send(new GetCardDetailsQuery(userId, cardNumber));
                return result == null ? NotFound($"Card {cardNumber} not found for user {userId}") : Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving card details for user {UserId}, card {CardNumber}", userId, cardNumber);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserCardsResponse>> GetUserCards(
            string userId,
            [FromQuery] CardType? cardType = null,
            [FromQuery] CardStatus? cardStatus = null,
            [FromQuery] bool? isPinSet = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page < 1) return BadRequest("Page must be greater than 0");
            if (pageSize < 1 || pageSize > 100) return BadRequest("Page size must be between 1 and 100");
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("User ID is required");

            try
            {
                var result = await _mediator.Send(new GetUserCardsQuery(userId, cardType, cardStatus, isPinSet, page, pageSize));

                if (!result.Cards.Any())
                {
                    var filters = new List<string>();
                    if (cardType.HasValue) filters.Add($"type: {cardType}");
                    if (cardStatus.HasValue) filters.Add($"status: {cardStatus}");
                    if (isPinSet.HasValue) filters.Add($"PIN: {isPinSet}");

                    var filterMessage = filters.Any() ? $" with filters: {string.Join(", ", filters)}" : "";
                    return NotFound($"No cards found for user {userId}{filterMessage}");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user cards for {UserId}", userId);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}