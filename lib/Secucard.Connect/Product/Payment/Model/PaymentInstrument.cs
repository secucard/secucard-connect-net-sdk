namespace Secucard.Connect.Product.Payment.Model
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.Serialization;

    [DataContract]
    public class PaymentInstrument
    {
        public const string PaymentInstrumentTypeBankAccount = "bank_account";
        public const string PaymentInstrumentTypeCreditCard = "credit_card";

        [DataMember(Name = "data")]
        public NameValueCollection Data { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        public PaymentInstrument CreateBankAccount(string owner, string iban, string bic = null, string bankname = null)
        {
            string maskedIban = iban.Replace(" ", string.Empty);
            string maskChars = new string('X', maskedIban.Length - 8);
            maskedIban = maskedIban.Substring(0, 4) + maskChars + maskedIban.Substring(maskedIban.Length - 4, 4);

            this.Type = PaymentInstrumentTypeBankAccount;
            this.Data = new NameValueCollection()
            {
                { "owner", owner },
                { "iban", maskedIban },
                { "bic", bic },
                { "bankname", bankname }
            };
            return this;
        }

        public PaymentInstrument CreateCreditCard(string owner, string pan, DateTime expiration_date, string issuer = null)
        {
            string maskedPan = pan.Replace(" ", string.Empty);
            string maskChars = new string('X', 8);
            maskedPan = maskedPan.Substring(0, 4) + maskChars;

            this.Type = PaymentInstrumentTypeCreditCard;
            this.Data = new NameValueCollection()
            {
                { "owner", owner },
                { "pan", maskedPan },
                { "expiration_date", expiration_date.ToString("yyyy-MM-dd\\T23:59:59") },
                { "issuer", issuer }
            };
            return this;
        }

        public override string ToString()
        {
            return "PaymentInstrument{" +
                   "data='" + this.Data + '\'' +
                   ", type='" + this.Type + '\'' +
                   '}';
        }
    }
}