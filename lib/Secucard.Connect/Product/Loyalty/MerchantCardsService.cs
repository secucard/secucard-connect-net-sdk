namespace Secucard.Connect.Product.Loyalty
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Loyalty.Model;

    public class MerchantCardsService : ProductService<MerchantCard>
    {
        public static readonly ServiceMetaData<MerchantCard> MetaData = new ServiceMetaData<MerchantCard>(
            "loyalty", "merchantcards");

        /// <summary>
        /// Check the given CSC
        /// </summary>
        /// <param name="cardNumber">Number of the card</param>
        /// <param name="csc">CSC number</param>
        /// <returns></returns>
        public bool ValidateCSC(string cardNumber, string csc)
        {
            var data = new { cardnumber = cardNumber, csc = csc };
            return this.ExecuteToBool("me", "CheckCsc", null, data, null);
        }

        /// <summary>
        /// Check the given passcode
        /// </summary>
        /// <param name="cardNumber">Number of the card</param>
        /// <param name="pin">PIN number</param>
        /// <returns></returns>
        public bool ValidatePasscode(string cardNumber, string pin)
        {
            var data = new { cardnumber = cardNumber, pin = pin };
            return this.ExecuteToBool("me", "CheckPasscode", null, data, null);
        }

        protected override ServiceMetaData<MerchantCard> GetMetaData()
        {
            return MetaData;
        }
    }
}