namespace Secucard.Connect.Product.Loyalty
{
    using System;
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Loyalty.Model;

    public class CardGroupsService : ProductService<CardGroup>
    {
        /// <summary>
        /// Check if a passcode is enabled for this card
        /// </summary>
        /// <param name="cardGroupId">CRG_XYZ</param>
        /// <param name="transactionType">Type of transaction</param>
        /// <param name="cardNumber">Number of the card</param>
        /// <returns></returns>
        public bool CheckPasscodeEnabled(string cardGroupId, string transactionType, string cardNumber)
        {
            var data = new { action = transactionType, cardnumber = cardNumber };
            return this.ExecuteToBool(cardGroupId, "checkPasscodeEnabled", null, data, null);
        }

        protected override ServiceMetaData<CardGroup> GetMetaData()
        {
            throw new NotImplementedException();
        }
    }
}
