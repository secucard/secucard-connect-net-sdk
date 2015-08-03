namespace Secucard.Connect.Product.Payment
{
    using System.Diagnostics;
    using Secucard.Connect.Client;
    using Secucard.Model.Payment;

    public class ContractService : ProductService<Contract>
    {
        //public Contract CloneMyContract(CloneParams cloneParams)
        //{
        //    return GetChannel().Execute<Contract, CloneParams>("me", "clone", null, cloneParams);
        //}

        //public Contract CloneContract(string contractId, CloneParams cloneParams)
        //{
        //    return GetChannel().Execute<Contract, CloneParams>(contractId, "clone", null, cloneParams);
        //}

        protected override ServiceMetaData<Contract> CreateMetaData()
        {
            return new ServiceMetaData<Contract>("payment", "contracts");
        }
    }
}
