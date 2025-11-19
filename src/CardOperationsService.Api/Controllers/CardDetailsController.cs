using CardOperationsService.Application.Cards.Queries.GetCardDetails;
using CardOperationsService.Application.Cards.Queries.GetUserCards;
using CardOperationsService.Contracts.CardDetails;
using CardOperationsService.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;



namespace CardOperationsService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CardDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}/{cardNumber}")]
        public async Task<ActionResult<CardDetailsResponse>> GetCardDetails(string userId, string cardNumber)
        {
            var result = await _mediator.Send(new GetCardDetailsQuery(userId, cardNumber));

            if (result == null)
                return NotFound();

            return Ok(result);
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
            var result = await _mediator.Send(new GetUserCardsQuery(
                userId,
                cardType,
                cardStatus,
                isPinSet,
                page,
                pageSize
            ));

            if (result == null || !result.Cards.Any())
            {
                return NotFound($"No cards found for user {userId} with specified filters");
            }

            return Ok(result);
        }
    }
}