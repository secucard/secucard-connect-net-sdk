namespace Secucard.Model.Payment
{
    using System.Runtime.Serialization;

    [DataContract]
    public class TransferAccount
    {

        [DataMember(Name = "account_owner")]
        public string AccountOwner;

        [DataMember(Name = "accountnumber")]
        public string AccountNumber;

        [DataMember(Name = "iban")]
        public string Iban;

        [DataMember(Name = "bic")]
        public string Bic;

        [DataMember(Name = "bankcode")]
        public string BankCode;

        public override string ToString()
        {
            return "TransferAccount{" +
                   "accountOwner='" + AccountOwner + '\'' +
                   ", accountNumber='" + AccountNumber + '\'' +
                   ", iban='" + Iban + '\'' +
                   ", bic='" + Bic + '\'' +
                   ", bankCode='" + BankCode + '\'' +
                   '}';
        }
    }
}
