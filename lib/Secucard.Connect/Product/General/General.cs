namespace Secucard.Connect.Product.General
{
    /// <summary>
    /// Holds service references and service type constants for "general" product
    /// </summary>
    public class General
    {
        public AccountDevicesService Accountdevices { get; set; }
        public AccountsService Accounts { get; set; }
        public MerchantsService Merchants { get; set; }
        public NewsService News { get; set; }
        public PublicMerchantsService Publicmerchants { get; set; }
        public StoresService Stores { get; set; }
        public GeneralTransactionsService GeneralTransactions { get; set; }
    }
}