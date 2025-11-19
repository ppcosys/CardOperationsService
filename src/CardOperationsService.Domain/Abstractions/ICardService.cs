using CardOperationsService.Domain.Entities;
using CardOperationsService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOperationsService.Domain.Abstractions
{
    public interface ICardService
    {
        Task<CardDetails?> GetCardDetails(string userId, string cardNumber);
        Task<IEnumerable<CardDetails>> GetUserCards(string userId);

        Task<(IEnumerable<CardDetails> Cards, int TotalCount)> GetUserCardsWithFilters(
            string userId,
            CardType? cardType = null,
            CardStatus? cardStatus = null,
            bool? isPinSet = null,
            int page = 1,
            int pageSize = 10);
    }
}
