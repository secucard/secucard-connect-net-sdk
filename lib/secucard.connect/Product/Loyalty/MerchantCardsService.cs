
using Secucard.Connect.Client;
using Secucard.Model.Loyalty;

public class MerchantCardsService : ProductService<MerchantCard> {

    protected override ServiceMetaData<MerchantCard> CreateMetaData()
    {
        return new ServiceMetaData<MerchantCard>("loyalty", "merchantcards");
    }


}
