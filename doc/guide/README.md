## Usage

```csharp
// Load default properties
var properties = Properties.Load("SecucardConnect.config");

// Perpare client config. Implement your own Auth Details
clientConfiguration = new ClientConfiguration(properties)
{
    ClientAuthDetails = new ClientAuthDetailsDeviceToBeImplemented(),
    DataStorage = new MemoryDataStorage()
};

// Create client and attach client event handlers
var Client = SecucardConnect.Create(clientConfiguration);
Client.AuthEvent += ClientOnAuthEvent;
Client.ConnectionStateChangedEvent += ClientOnConnectionStateChangedEvent;
Client.Open();

// register at smart.checkin events (incoming customers)
var checkinService = Client.Smart.Checkins;
checkinService.CheckinEvent += CheckinEvent;

// get reference to transaction and ident service
var transactionService = Client.Smart.Transactions;
var identService = Client.Smart.Idents;

// register eventhandler für transaction service --> progress during transaction
transactionService.TransactionCashierEvent += SmartTransactionCashierEvent;

// select an ident
var availableIdents = identService.GetList(null);
if (availableIdents == null || availableIdents.Count == 0)
{
    throw new Exception("No idents found.");
}
var ident = availableIdents.List.First(o => o.Id == "smi_1");
ident.Value = "pdo28hdal";

var selectedIdents = new List<Ident> {ident};

// prepare basket with items locally
var groups = new List<ProductGroup>
{
    new ProductGroup {Id = "group1", Desc = "beverages", Level = 1}
};

var basket = new Basket();
basket.AddProduct(new Product
{
    Id = 1,
    ArticleNumber = "3378",
    Ean = "5060215249804",
    Desc = "desc1",
    Quantity = 5m,
    PriceOne = 1999,
    Tax = 7,
    Groups = groups
});

var basketInfo = new BasketInfo {Sum = 1, Currency = "EUR"};

// build transaction object
var newTrans = new Transaction
{
    BasketInfo = basketInfo,
    Basket = basket,
    Idents = selectedIdents,
    MerchantRef = "merchant21",
    TransactionRef = "transaction99"
};

// create transaction on server
var transaction = transactionService.Create(newTrans);

// start transaction (this takes some time, consider another thread) 
var result = transactionService.Start(transaction.Id, "demo");

// cancel transaction if needed
var b = transactionService.Cancel(transaction.Id);
```

Available service classes are in namespace [Secucard.Connect.Product](https://github.com/secucard/secucard-connect-net-sdk/tree/master/lib/secucard.connect/Product)

For your own configuration file see: [lib\Secucard.Connect\Client\Config\SecucardConnect.xml](https://github.com/secucard/secucard-connect-net-sdk/blob/master/lib/Secucard.Connect/Client/Config/SecucardConnect.config)
