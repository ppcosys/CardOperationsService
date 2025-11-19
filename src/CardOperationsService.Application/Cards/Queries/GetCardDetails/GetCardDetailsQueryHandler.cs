using CardOperationsService.Application.Cards.Queries.GetCardDetails;
using CardOperationsService.Domain.Entities;
using CardOperationsService.Domain.Abstractions;
using MediatR;
using CardOperationsService.Contracts.CardDetails;

namespace CardOperationsService.Application.Cards.Queries.GetCardDetails
{
    public class GetCardDetailsQueryHandler : IRequestHandler<GetCardDetailsQuery, CardDetailsResponse?>
    {
        private readonly ICardService _cardService;

        public GetCardDetailsQueryHandler(ICardService cardService)
        {
            _cardService = cardService;
        }

        public async Task<CardDetailsResponse?> Handle(GetCardDetailsQuery request, CancellationToken cancellationToken)
        {
            var cardDetails = await _cardService.GetCardDetails(request.UserId, request.CardNumber);
            
            if (cardDetails == null)
                return null;

            
            return new CardDetailsResponse(
                CardNumber: cardDetails.CardNumber,
                CardType: cardDetails.CardType.ToString(),
                CardStatus: cardDetails.CardStatus.ToString(),
                IsPinSet: cardDetails.IsPinSet
            );
        }
    }
}