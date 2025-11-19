using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardOperationsService.Contracts.CardDetails;

namespace CardOperationsService.Application.Cards.Queries.GetUserCards
{
    public record GetUserCardsQuery(string UserId) : IRequest<UserCardsResponse>;
}
