using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOperationsService.Contracts.CardDetails
{
    public record CardDetailsResponse(
        string CardNumber,
        string CardType,
        string CardStatus,
        bool IsPinSet
    );
}
