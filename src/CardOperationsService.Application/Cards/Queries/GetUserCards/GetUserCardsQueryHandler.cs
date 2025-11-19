using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardOperationsService.Application.Cards.Queries.GetUserCards;
using CardOperationsService.Domain.Abstractions;
using CardOperationsService.Contracts.CardDetails;
using MediatR;
using CardOperationsService.Domain.Enums;

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
            var (cards, totalCount) = await _cardService.GetUserCardsWithFilters(
                request.UserId,
                request.CardType,
                request.CardStatus,
                request.IsPinSet,
                request.Page,
                request.PageSize
            );

            var cardResponses = cards.Select(card => new CardDetailsResponse(
                CardNumber: card.CardNumber,
                CardType: card.CardType.ToString(),
                CardStatus: card.CardStatus.ToString(),
                IsPinSet: card.IsPinSet
            ));

            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            return new UserCardsResponse(
                UserId: request.UserId,
                Cards: cardResponses,
                TotalCount: totalCount,
                Page: request.Page,
                PageSize: request.PageSize,
                TotalPages: totalPages,
                CardTypeFilter: request.CardType,
                CardStatusFilter: request.CardStatus,
                IsPinSetFilter: request.IsPinSet
            );
        }
    }
}
