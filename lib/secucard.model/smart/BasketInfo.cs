namespace Secucard.Model.Smart
{
    using System.Runtime.Serialization;

    [DataContract]
    public class BasketInfo
    {

        [DataMember(Name = "sum")]
        public int Sum;


        //private Currency currency;

        //public BasketInfo() {
        //}

        //public BasketInfo(int sum, Currency currency) {
        //  this.sum = sum;
        //  this.currency = currency;
        //}

        //public BasketInfo(int sum, String currencyCode) {
        //  this.sum = sum;
        //  this.currency = Currency.getInstance(currencyCode);
        //}



        public override string ToString()
        {
            return "BasketInfo{" +
                   "sum=" + Sum +
                   '}';
        }
    }
}
