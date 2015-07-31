namespace Secucard.Model.Payment
{
    using System.Runtime.Serialization;

    [DataContract]
    public class TransferAccount
    {
        [DataMember(Name = "accountnumber")]
        public string AccountNumber { get; set; }

        [DataMember(Name = "account_owner")]
        public string AccountOwner { get; set; }

        [DataMember(Name = "bankcode")]
        public string BankCode { get; set; }

        [DataMember(Name = "bic")]
        public string Bic { get; set; }

        [DataMember(Name = "iban")]
        public string Iban { get; set; }

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