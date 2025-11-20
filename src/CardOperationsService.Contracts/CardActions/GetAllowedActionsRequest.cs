using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardOperationsService.Domain.Enums;

namespace CardOperationsService.Contracts.CardActions
{
    public class GetAllowedActionsRequest
    {
        public CardType CardType { get; set; }
        public CardStatus CardStatus { get; set; }
        public bool IsPinSet { get; set; }
    }
}

