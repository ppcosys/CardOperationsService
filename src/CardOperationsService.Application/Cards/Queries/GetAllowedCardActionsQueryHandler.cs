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
    public class GetAllowedCardActionsQueryHandler : IRequestHandler<GetAllowedCardActionsQuery, List<string>>
    {
        public Task<List<string>> Handle(GetAllowedCardActionsQuery request, CancellationToken cancellationToken)
        {
            var allowedActions = CardRules.GetAllowedActions(request.Card)
                .Select(action => action.ToString().ToUpper())
                .ToList();

            return Task.FromResult(allowedActions);
        }
    }
}
