using CardOperationsService.Domain.Entities;
using CardOperationsService.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace CardOperationsService.Tests
{
    public class CardRulesTests
    {
        [Fact]
        public void Should_Allow_CorrectActions_For_Credit_Active_WithPin()
        {
            var card = new CardDetails("TEST123", CardType.Credit, CardStatus.Active, true);

            var result = CardRules.GetAllowedActions(card);

            result.Should().BeEquivalentTo(new[]
            {
                CardAction.Action1,
                CardAction.Action3,
                CardAction.Action4,
                CardAction.Action5,
                CardAction.Action6,
                CardAction.Action8,
                CardAction.Action9,
                CardAction.Action10,
                CardAction.Action11,
                CardAction.Action12,
                CardAction.Action13
            });
        }

        [Fact]
        public void Should_Allow_CorrectActions_For_Prepaid_Ordered_WithoutPin()
        {
            var card = new CardDetails("TEST123", CardType.Prepaid, CardStatus.Ordered, false);

            var result = CardRules.GetAllowedActions(card);

            result.Should().BeEquivalentTo(new[]
            {
                CardAction.Action3,
                CardAction.Action4,
                CardAction.Action7,
                CardAction.Action8,
                CardAction.Action9,
                CardAction.Action10,
                CardAction.Action12,
                CardAction.Action13
            });
        }

        [Fact]
        public void Should_Return_EmptyList_For_Unsupported_Type()
        {
            var card = new CardDetails("TEST123", (CardType)999, CardStatus.Active, true);

            var result = CardRules.GetAllowedActions(card);

            result.Should().BeEmpty();
        }

        [Fact]
        public void Should_Return_EmptyList_For_Unsupported_Status()
        {
            var card = new CardDetails("TEST123", CardType.Credit, (CardStatus)999, true);

            var result = CardRules.GetAllowedActions(card);

            result.Should().BeEmpty();
        }

        [Fact]
        public void Should_Allow_CorrectActions_For_Credit_Blocked_WithPin()
        {
            var card = new CardDetails("TEST123", CardType.Credit, CardStatus.Blocked, true);

            var result = CardRules.GetAllowedActions(card);

            result.Should().BeEquivalentTo(new[]
            {
                CardAction.Action3,
                CardAction.Action4,
                CardAction.Action5,
                CardAction.Action6,
                CardAction.Action7,
                CardAction.Action8,
                CardAction.Action9
            });
        }

        [Fact]
        public void Should_Allow_CorrectActions_For_Credit_Blocked_WithoutPin()
        {
            var card = new CardDetails("TEST123", CardType.Credit, CardStatus.Blocked, false);

            var result = CardRules.GetAllowedActions(card);

            result.Should().BeEquivalentTo(new[]
            {
                CardAction.Action3,
                CardAction.Action4,
                CardAction.Action5,
                CardAction.Action8,
                CardAction.Action9
            });
        }

        [Fact]
        public void Should_Allow_CorrectActions_For_Credit_Inactive_WithPin()
        {
            var card = new CardDetails("TEST123", CardType.Credit, CardStatus.Inactive, true);

            var result = CardRules.GetAllowedActions(card);

            result.Should().BeEquivalentTo(new[]
            {
                CardAction.Action2,
                CardAction.Action3,
                CardAction.Action4,
                CardAction.Action5,
                CardAction.Action6,
                CardAction.Action8,
                CardAction.Action9,
                CardAction.Action10,
                CardAction.Action11,
                CardAction.Action12,
                CardAction.Action13
            });
        }

        [Fact]
        public void Should_Allow_CorrectActions_For_Credit_Inactive_WithoutPin()
        {
            var card = new CardDetails("TEST123", CardType.Credit, CardStatus.Inactive, false);

            var result = CardRules.GetAllowedActions(card);

            result.Should().BeEquivalentTo(new[]
            {
                CardAction.Action2,
                CardAction.Action3,
                CardAction.Action4,
                CardAction.Action5,
                CardAction.Action7,
                CardAction.Action8,
                CardAction.Action9,
                CardAction.Action10,
                CardAction.Action11,
                CardAction.Action12,
                CardAction.Action13
            });
        }

        [Fact]
        public void Should_Allow_CorrectActions_For_Debit_Restricted()
        {
            var card = new CardDetails("TEST123", CardType.Debit, CardStatus.Restricted, true);

            var result = CardRules.GetAllowedActions(card);

            result.Should().BeEquivalentTo(new[]
            {
                CardAction.Action3,
                CardAction.Action4,
                CardAction.Action9
            });
        }

        [Fact]
        public void Should_Allow_CorrectActions_For_Prepaid_Closed()
        {
            var card = new CardDetails("TEST123", CardType.Prepaid, CardStatus.Closed, false);

            var result = CardRules.GetAllowedActions(card);

            result.Should().BeEquivalentTo(new[]
            {
                CardAction.Action3,
                CardAction.Action4,
                CardAction.Action9
            });
        }
    }
}