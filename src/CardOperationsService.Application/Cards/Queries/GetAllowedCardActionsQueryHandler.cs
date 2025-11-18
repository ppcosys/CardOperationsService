using CardOperationsService.Domain.Entities;
using CardOperationsService.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOperationsService.Application.Cards.Queries
{
    public class GetAllowedCardActionsQueryHandler : IRequestHandler<GetAllowedCardActionsQuery, List<CardAction>>
    {
        public Task<List<CardAction>> Handle(GetAllowedCardActionsQuery request, CancellationToken cancellationToken)
        {
            var allowedActions = CardRules.GetAllowedActions(request.Card);
            return Task.FromResult(allowedActions);
        }
    }
}
