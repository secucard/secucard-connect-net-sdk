namespace Secucard.Connect.Product.Loyalty
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Loyalty.Model;

    public class MerchantCardsService : ProductService<MerchantCard>
    {
        public static readonly ServiceMetaData<MerchantCard> MetaData = new ServiceMetaData<MerchantCard>(
            "loyalty", "merchantcards");

        public bool ValidateCSC(string cardNumber, string csc)
        {
            var data = new { cardnumber = cardNumber, csc = csc };
            return this.ExecuteToBool("me", "CheckCsc", null, data, null);
        }

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