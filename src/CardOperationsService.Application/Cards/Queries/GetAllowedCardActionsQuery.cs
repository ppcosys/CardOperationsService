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
    public record GetAllowedCardActionsQuery(CardDetails Card) : IRequest<List<CardAction>>;
}
