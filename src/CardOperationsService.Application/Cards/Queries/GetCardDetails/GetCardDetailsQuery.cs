using MediatR;
using CardOperationsService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardOperationsService.Contracts.CardDetails;

namespace CardOperationsService.Application.Cards.Queries.GetCardDetails
{
    public record GetCardDetailsQuery(string UserId, string CardNumber)
        : IRequest<CardDetailsResponse>;
}
