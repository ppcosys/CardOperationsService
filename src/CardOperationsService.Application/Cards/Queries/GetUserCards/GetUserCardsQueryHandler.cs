using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardOperationsService.Application.Cards.Queries.GetUserCards;
using CardOperationsService.Domain.Abstractions;
using CardOperationsService.Contracts.CardDetails;
using MediatR;

namespace CardOperationsService.Application.Cards.Queries.GetUserCards
{
    public class GetUserCardsQueryHandler : IRequestHandler<GetUserCardsQuery, UserCardsResponse>
    {
        private readonly ICardService _cardService;

        public GetUserCardsQueryHandler(ICardService cardService)
        {
            _cardService = cardService;
        }

        public async Task<UserCardsResponse> Handle(GetUserCardsQuery request, CancellationToken cancellationToken)
        {
            var cards = await _cardService.GetUserCards(request.UserId);

            var cardResponses = cards.Select(card => new CardDetailsResponse(
                CardNumber: card.CardNumber,
                CardType: card.CardType.ToString(),
                CardStatus: card.CardStatus.ToString(),
                IsPinSet: card.IsPinSet
            ));

            return new UserCardsResponse(request.UserId, cardResponses);
        }
    }
}
