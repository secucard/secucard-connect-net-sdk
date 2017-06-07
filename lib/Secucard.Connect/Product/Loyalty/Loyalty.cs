namespace Secucard.Connect.Product.Loyalty
{
    /// <summary>
    /// Holds service references and service type constants for "loyalty" product.
    /// </summary>
    public class Loyalty
    {
        public CardsService Cards { get; set; }

        public CustomerLoyaltyService CustomerLoyalty { get; set; }

        public MerchantCardsService Merchantcards { get; set; }
    }
}