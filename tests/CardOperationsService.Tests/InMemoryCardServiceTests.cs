using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardOperationsService.Domain.Enums;
using CardOperationsService.Infrastructure.Services;
using FluentAssertions;
using Xunit;

namespace CardOperationsService.Tests
{
    public class InMemoryCardServiceTests
    {
        private readonly InMemoryCardService _service;

        public InMemoryCardServiceTests()
        {
            _service = new InMemoryCardService();
        }

        [Fact]
        public async Task GetUserCardsWithFilters_WithTypeFilter_ReturnsFilteredCards()
        {
            // Arrange
            // Act
            var (cards, totalCount) = await _service.GetUserCardsWithFilters("User1", CardType.Credit);

            // Assert
            cards.Should().HaveCount(7); // 7 statusów dla Credit
            cards.Should().OnlyContain(c => c.CardType == CardType.Credit);
            totalCount.Should().Be(7);
        }

        [Fact]
        public async Task GetUserCardsWithFilters_WithPagination_ReturnsCorrectPage()
        {
            // Arrange
            // Act
            var (cards, totalCount) = await _service.GetUserCardsWithFilters("User1", page: 2, pageSize: 5);

            // Assert
            cards.Should().HaveCount(5);
            totalCount.Should().Be(21); // Wszystkie karty User1
        }

        [Fact]
        public async Task GetUserCardsWithFilters_WithMultipleFilters_ReturnsCorrectResults()
        {
            // Arrange
            // Act
            var (cards, totalCount) = await _service.GetUserCardsWithFilters(
                "User1",
                CardType.Debit,
                CardStatus.Active,
                true
            );

            // Assert
            cards.Should().OnlyContain(c =>
                c.CardType == CardType.Debit &&
                c.CardStatus == CardStatus.Active &&
                c.IsPinSet == true
            );
        }

        [Fact]
        public async Task GetUserCardsWithFilters_WithInvalidUser_ReturnsEmpty()
        {
            // Arrange
            // Act
            var (cards, totalCount) = await _service.GetUserCardsWithFilters("NonExistentUser");

            // Assert
            cards.Should().BeEmpty();
            totalCount.Should().Be(0);
        }

        [Fact]
        public async Task GetUserCardsWithFilters_WithStatusFilter_ReturnsFilteredCards()
        {
            // Arrange
            // Act
            var (cards, totalCount) = await _service.GetUserCardsWithFilters("User1", cardStatus: CardStatus.Active);

            // Assert
            cards.Should().HaveCount(3); // 3 typy kart dla statusu Active
            cards.Should().OnlyContain(c => c.CardStatus == CardStatus.Active);
            totalCount.Should().Be(3);
        }

        [Fact]
        public async Task GetUserCardsWithFilters_WithPinFilter_ReturnsFilteredCards()
        {
            // Arrange
            // Act
            var (cards, totalCount) = await _service.GetUserCardsWithFilters("User1", isPinSet: true);

            // Assert
            cards.Should().OnlyContain(c => c.IsPinSet == true);
            // Spodziewamy się około połowy kart z PINem (bo cardIndex % 2 == 0)
        }

        [Fact]
        public async Task GetUserCardsWithFilters_WithAllFilters_ReturnsCorrectResults()
        {
            // Arrange
            // Act
            var (cards, totalCount) = await _service.GetUserCardsWithFilters(
                "User1",
                CardType.Prepaid,
                CardStatus.Blocked,
                false,
                page: 1,
                pageSize: 10
            );

            // Assert
            cards.Should().OnlyContain(c =>
                c.CardType == CardType.Prepaid &&
                c.CardStatus == CardStatus.Blocked &&
                c.IsPinSet == false
            );
        }

        [Fact]
        public async Task GetCardDetails_WithValidData_ReturnsCard()
        {
            // Arrange
            // Act
            var result = await _service.GetCardDetails("User1", "Card11");

            // Assert
            result.Should().NotBeNull();
            result.CardNumber.Should().Be("Card11");
            result.CardType.Should().Be(CardType.Prepaid);
            result.CardStatus.Should().Be(CardStatus.Ordered);
            result.IsPinSet.Should().BeFalse();
        }

        [Fact]
        public async Task GetCardDetails_WithInvalidData_ReturnsNull()
        {
            // Arrange
            // Act
            var result = await _service.GetCardDetails("User1", "NonExistentCard");

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetUserCards_WithValidUser_ReturnsAllCards()
        {
            // Arrange
            // Act
            var result = await _service.GetUserCards("User1");

            // Assert
            result.Should().HaveCount(21); // 3 types × 7 statuses
        }
    }
}
