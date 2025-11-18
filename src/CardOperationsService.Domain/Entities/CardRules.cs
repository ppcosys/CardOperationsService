using CardOperationsService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOperationsService.Domain.Entities
{
    public static class CardRules
    {
        public static List<CardAction> GetAllowedActions(CardDetails card)
        {
            var actions = new List<CardAction>();

            // ACTION1
            if (IsYes(card, CardAction.Action1))
                actions.Add(CardAction.Action1);

            // ACTION2
            if (IsYes(card, CardAction.Action2))
                actions.Add(CardAction.Action2);

            // ACTION3
            if (IsYes(card, CardAction.Action3))
                actions.Add(CardAction.Action3);

            // ACTION4
            if (IsYes(card, CardAction.Action4))
                actions.Add(CardAction.Action4);

            // ACTION5
            if (IsYes(card, CardAction.Action5))
                actions.Add(CardAction.Action5);

            // ACTION6
            if (IsYes(card, CardAction.Action6))
                actions.Add(CardAction.Action6);

            // ACTION7
            if (IsYes(card, CardAction.Action7))
                actions.Add(CardAction.Action7);

            // ACTION8
            if (IsYes(card, CardAction.Action8))
                actions.Add(CardAction.Action8);

            // ACTION9
            if (IsYes(card, CardAction.Action9))
                actions.Add(CardAction.Action9);

            // ACTION10
            if (IsYes(card, CardAction.Action10))
                actions.Add(CardAction.Action10);

            // ACTION11
            if (IsYes(card, CardAction.Action11))
                actions.Add(CardAction.Action11);

            // ACTION12
            if (IsYes(card, CardAction.Action12))
                actions.Add(CardAction.Action12);

            // ACTION13
            if (IsYes(card, CardAction.Action13))
                actions.Add(CardAction.Action13);

            return actions;
        }

        private static bool IsYes(CardDetails card, CardAction action)
        {
            var t = card.CardType;
            var s = card.CardStatus;
            var pin = card.IsPinSet;

            return action switch
            {
                CardAction.Action1 => IsAllowed(t) && IsStatus(s, CardStatus.Active),
                CardAction.Action2 => IsAllowed(t) && IsStatus(s, CardStatus.Active) && t != CardType.Prepaid,
                CardAction.Action3 => IsAllowed(t) && IsStatus(s),
                CardAction.Action4 => IsAllowed(t) && IsStatus(s),
                CardAction.Action5 => t == CardType.Credit && IsStatus(s),
                CardAction.Action6 => IsAllowed(t) && IsStatus(s, CardStatus.Active, CardStatus.Inactive, CardStatus.Ordered, CardStatus.Blocked) && pin,
                CardAction.Action7 => IsAllowed(t) && IsStatus(s, CardStatus.Active, CardStatus.Inactive, CardStatus.Ordered, CardStatus.Blocked) && pin,
                CardAction.Action8 => IsAllowed(t) && IsStatus(s),
                CardAction.Action9 => IsAllowed(t) && IsStatus(s),
                CardAction.Action10 => false,
                CardAction.Action11 => false,
                CardAction.Action12 => IsAllowed(t) && IsStatus(s, CardStatus.Active, CardStatus.Inactive, CardStatus.Ordered),
                CardAction.Action13 => IsAllowed(t) && IsStatus(s, CardStatus.Active, CardStatus.Inactive, CardStatus.Ordered),
                _ => false
            };
        }

        private static bool IsAllowed(CardType t)
            => t == CardType.Prepaid || t == CardType.Debit || t == CardType.Credit;

        private static bool IsStatus(CardStatus s, params CardStatus[] statuses)
            => statuses.Length == 0 || statuses.Contains(s);
    }
}
