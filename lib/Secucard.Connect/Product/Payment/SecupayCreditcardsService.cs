namespace Secucard.Connect.Product.Payment
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.General.Model;
    using Secucard.Connect.Product.Payment.Model;
    using Secucard.Connect.Product.Payment.Event;

    public class SecupayCreditcardsService : ProductService<SecupayCreditcard>
    {
        public static readonly ServiceMetaData<SecupayCreditcard> MetaData = new ServiceMetaData<SecupayCreditcard>("payment", "secupaycreditcards");

        public PaymentEventHandler PaymentEvent;

        public bool Cancel(string prepayId, string contractId = null, int amount = 0)
        {
            var data = new { contract = contractId, amount = amount };

            return this.Execute<Model.Transaction>(prepayId, "cancel", null, data, null) != null;
        }

        public bool Capture(string paymentId)
        {
            return false;
        }

        public bool UpdateBasket(string paymentId, Basket[] basket)
        {
            return false;
        }

        protected override ServiceMetaData<SecupayCreditcard> GetMetaData()
        {
            return MetaData;
        }

        public override void RegisterEvents()
        {
            Context.EventDispatcher.RegisterForEvent(GetType().Name, "payment.secupaycreditcards", "changed", OnChangedEvent);
        }

        private void OnChangedEvent(object obj)
        {
            var t = (Event<SecupayCreditcard[]>)obj;

            // Get the current data of the changed payment transaction
            for (int key = 0; key < t.Data.Length; ++key)
            {
                t.Data[key] = this.Get(t.Data[key].Id);
            }

            // Handle event
            PaymentEvent(this, new PaymentEventEventArgs { SecucardEvent = t });
        }
    }
}