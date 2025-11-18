using CardOperationsService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOperationsService.Domain.Entities
{
    public record CardDetails(
        string CardNumber,
        CardType CardType,
        CardStatus CardStatus,
        bool IsPinSet
    );
}
