using CardOperationsService.Application.Cards.Queries;
using CardOperationsService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CardOperationsService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardActionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CardActionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("allowed")]
        public async Task<IActionResult> GetAllowedActions([FromBody] CardDetails card)
        {
            var query = new GetAllowedCardActionsQuery(card);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
