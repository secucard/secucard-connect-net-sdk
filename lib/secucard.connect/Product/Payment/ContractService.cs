namespace Secucard.Connect.Product.General
{
    using Secucard.Model.Payment;

    public class ContractService : AbstractService
    {
        public Contract CloneMyContract(CloneParams cloneParams)
        {
            return GetChannel().Execute<Contract, CloneParams>("me", "clone", null, cloneParams);
        }

        public Contract CloneContract(string contractId, CloneParams cloneParams)
        {
            return GetChannel().Execute<Contract, CloneParams>(contractId, "clone", null, cloneParams);
        }

    }
}
