using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOperationsService.Contracts.CardDetails
{
    public record UserCardsResponse(
        string UserId,
        IEnumerable<CardDetailsResponse> Cards
    );
}
