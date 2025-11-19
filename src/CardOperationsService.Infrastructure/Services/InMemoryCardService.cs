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