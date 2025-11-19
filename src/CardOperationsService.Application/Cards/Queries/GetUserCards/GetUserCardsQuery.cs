using CardOperationsService.Contracts.CardDetails;
using CardOperationsService.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CardOperationsService.Application.Cards.Queries.GetUserCards
{
    public record GetUserCardsQuery(
        string UserId,
        CardType? CardType = null,
        CardStatus? CardStatus = null,
        bool? IsPinSet = null,
        int Page = 1,
        int PageSize = 10
    ) : IRequest<UserCardsResponse>;
}
