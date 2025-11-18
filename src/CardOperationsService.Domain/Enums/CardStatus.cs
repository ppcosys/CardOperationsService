using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOperationsService.Domain.Enums
{
    public enum CardStatus
    {
        Ordered,
        Inactive,
        Active,
        Restricted,
        Blocked,
        Expired,
        Closed
    }
}
