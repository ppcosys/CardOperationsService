using CardOperationsService.Domain.Abstractions;
using CardOperationsService.Domain.Entities;
using CardOperationsService.Domain.Enums;

namespace CardOperationsService.Infrastructure.Services
{
    public class InMemoryCardService : ICardService
    {
        private readonly Dictionary<string, Dictionary<string, CardDetails>> _userCards;

        public InMemoryCardService()
        {
            _userCards = CreateSampleUserCards();
        }

        public async Task<CardDetails?> GetCardDetails(string userId, string cardNumber)
        {
            await Task.Delay(100);

            if (!_userCards.TryGetValue(userId, out var cards) ||
                !cards.TryGetValue(cardNumber, out var card))
            {
                return null;
            }

            return card;
        }

        public async Task<IEnumerable<CardDetails>> GetUserCards(string userId)
        {
            await Task.Delay(50);

            if (_userCards.TryGetValue(userId, out var cards))
            {
                return cards.Values;
            }

            return Enumerable.Empty<CardDetails>();
        }

        public async Task<(IEnumerable<CardDetails> Cards, int TotalCount)> GetUserCardsWithFilters(
            string userId,
            CardType? cardType = null,
            CardStatus? cardStatus = null,
            bool? isPinSet = null,
            int page = 1,
            int pageSize = 10)
        {
            await Task.Delay(50);

            if (!_userCards.TryGetValue(userId, out var cardsDict))
            {
                return (Enumerable.Empty<CardDetails>(), 0);
            }

            var cards = cardsDict.Values.AsEnumerable();

            
            if (cardType.HasValue)
            {
                cards = cards.Where(c => c.CardType == cardType.Value);
            }

            if (cardStatus.HasValue)
            {
                cards = cards.Where(c => c.CardStatus == cardStatus.Value);
            }

            if (isPinSet.HasValue)
            {
                cards = cards.Where(c => c.IsPinSet == isPinSet.Value);
            }

            var totalCount = cards.Count();

            
            var pagedCards = cards
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (pagedCards, totalCount);
        }

        private static Dictionary<string, Dictionary<string, CardDetails>> CreateSampleUserCards()
        {
            var userCards = new Dictionary<string, Dictionary<string, CardDetails>>();

            for (int i = 1; i <= 3; i++)
            {
                var cards = new Dictionary<string, CardDetails>();
                int cardIndex = 1;

                foreach (CardType cardType in Enum.GetValues(typeof(CardType)))
                {
                    foreach (CardStatus cardStatus in Enum.GetValues(typeof(CardStatus)))
                    {
                        var cardNumber = $"Card{i}{cardIndex}";
                        cards[cardNumber] = new CardDetails(
                            CardNumber: cardNumber,
                            CardType: cardType,
                            CardStatus: cardStatus,
                            IsPinSet: cardIndex % 2 == 0
                        );
                        cardIndex++;
                    }
                }

                userCards.Add($"User{i}", cards);
            }

            return userCards;
        }
    }
}