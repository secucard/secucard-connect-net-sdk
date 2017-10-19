namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Payment.Model;

    public class ContractService : ProductService<Contract>
    {
        public static readonly ServiceMetaData<Contract> MetaData = new ServiceMetaData<Contract>("payment", "contracts");

        public Contract CloneMyContract(CloneParams cloneParams)
        {
            return this.Execute<Contract>("me", "clone", null, cloneParams, null);
        }

        public Contract Clone(string contractId, CloneParams cloneParams)
        {
            return this.Execute<Contract>(contractId, "clone", null, cloneParams, null);
        }

        protected override ServiceMetaData<Contract> GetMetaData()
        {
            return MetaData;
        }
    }
}