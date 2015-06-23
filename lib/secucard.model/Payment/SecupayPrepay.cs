namespace Secucard.Model.Payment
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SecupayPrepay : Transaction
    {
        [DataMember(Name = "transfer_purpose")] public string TransferPurpose;

        [DataMember(Name = "transfer_account")] public TransferAccount TransferAccount;

        public override string ToString()
        {
            return "SecupayPrepay{" +
                   "transferPurpose='" + TransferPurpose + '\'' +
                   ", transactionStatus='" + transactionStatus + '\'' +
                   ", transferAccount=" + TransferAccount +
                   "} " + base.ToString();
        }

        public override string SecuObjectName
        {
            get { return "payment.secupayprepays"; }
        }
    }
}