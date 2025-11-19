using CardOperationsService.Domain.Enums;

namespace CardOperationsService.Domain.Entities
{
    public static class CardRules
    {
        public static List<CardAction> GetAllowedActions(CardDetails card)
        {
            var actions = new List<CardAction>();

            foreach (CardAction action in Enum.GetValues(typeof(CardAction)))
            {
                if (IsActionAllowed(card, action))
                    actions.Add(action);
            }

            return actions;
        }

        private static bool IsActionAllowed(CardDetails card, CardAction action)
        {
            if (!IsValidCardType(card.CardType) || !IsValidCardStatus(card.CardStatus))
                return false;

            return action switch
            {
                // ACTION1:
                CardAction.Action1 => card.CardStatus == CardStatus.Active,

                // ACTION2:
                CardAction.Action2 => card.CardStatus == CardStatus.Inactive,

                // ACTION3:
                CardAction.Action3 => true,

                // ACTION4:
                CardAction.Action4 => true,

                // ACTION5:
                CardAction.Action5 => card.CardType == CardType.Credit,

                // ACTION6:
                CardAction.Action6 => (card.CardStatus == CardStatus.Ordered ||
                                      card.CardStatus == CardStatus.Inactive ||
                                      card.CardStatus == CardStatus.Active ||
                                      card.CardStatus == CardStatus.Blocked) && card.IsPinSet,

                // ACTION7:
                CardAction.Action7 => ((card.CardStatus == CardStatus.Ordered ||
                                       card.CardStatus == CardStatus.Inactive ||
                                       card.CardStatus == CardStatus.Active) && !card.IsPinSet) ||
                                      (card.CardStatus == CardStatus.Blocked && card.IsPinSet),

                // ACTION8:
                CardAction.Action8 => card.CardStatus == CardStatus.Ordered ||
                                     card.CardStatus == CardStatus.Inactive ||
                                     card.CardStatus == CardStatus.Active ||
                                     card.CardStatus == CardStatus.Blocked,

                // ACTION9:
                CardAction.Action9 => true,

                // ACTION10:
                CardAction.Action10 => card.CardStatus == CardStatus.Ordered ||
                                      card.CardStatus == CardStatus.Inactive ||
                                      card.CardStatus == CardStatus.Active,

                // ACTION11:
                CardAction.Action11 => card.CardStatus == CardStatus.Inactive ||
                                      card.CardStatus == CardStatus.Active,

                // ACTION12:
                CardAction.Action12 => card.CardStatus == CardStatus.Ordered ||
                                      card.CardStatus == CardStatus.Inactive ||
                                      card.CardStatus == CardStatus.Active,

                // ACTION13:
                CardAction.Action13 => card.CardStatus == CardStatus.Ordered ||
                                      card.CardStatus == CardStatus.Inactive ||
                                      card.CardStatus == CardStatus.Active,

                _ => false
            };
        }

        private static bool IsValidCardType(CardType cardType)
        {
            return cardType == CardType.Prepaid ||
                   cardType == CardType.Debit ||
                   cardType == CardType.Credit;
        }

        private static bool IsValidCardStatus(CardStatus cardStatus)
        {
            return cardStatus == CardStatus.Ordered ||
                   cardStatus == CardStatus.Inactive ||
                   cardStatus == CardStatus.Active ||
                   cardStatus == CardStatus.Restricted ||
                   cardStatus == CardStatus.Blocked ||
                   cardStatus == CardStatus.Expired ||
                   cardStatus == CardStatus.Closed;
        }
    }
}