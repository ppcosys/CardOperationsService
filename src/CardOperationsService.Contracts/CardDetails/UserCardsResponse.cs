using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardOperationsService.Domain.Enums;

namespace CardOperationsService.Contracts.CardDetails
{
    public record UserCardsResponse(
        string UserId,
        IEnumerable<CardDetailsResponse> Cards,
        int TotalCount,
        int Page,
        int PageSize,
        int TotalPages,
        CardType? CardTypeFilter = null,
        CardStatus? CardStatusFilter = null,
        bool? IsPinSetFilter = null
    );
}
