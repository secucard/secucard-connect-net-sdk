namespace Secucard.Connect.Product.Payment
{
    using System;
    using Secucard.Connect.Client;
    using Secucard.Connect.Event;
    using Secucard.Connect.Product.General.Model;
    using Secucard.Connect.Product.Payment.Event;
    using Secucard.Connect.Product.Payment.Model;

    public abstract class PaymentService<T> : ProductService<T> where T : Model.Transaction
    {
        public PaymentEventHandler<T> PaymentEvent { get; set; }

        public bool Cancel(string paymentId, string contractId = null, int amount = 0)
        {
            T data = this.CreateModelInstance();
            data.Id = paymentId;
            data.Amount = amount;
            data.Contract = new Contract { Id = contractId };

            return this.Execute<Model.Transaction>(paymentId, "cancel", null, data, null) != null;
        }
        
        public bool Capture(string paymentId)
        {
            return this.Execute<Model.Transaction>(paymentId, "capture", null, null, null) != null;
        }

        public bool UpdateBasket(string paymentId, Basket[] basket)
        {
            T data = this.CreateModelInstance();
            data.Id = paymentId;
            data.Basket = basket;

            return this.Execute<Model.Transaction>(paymentId, "basket", null, data, null) != null;
        }

        public bool ReverseAccrual(string paymentId)
        {
            T data = this.CreateModelInstance();
            data.Id = paymentId;
            data.Accrual = false;

            return this.Execute<Model.Transaction>(paymentId, "accrual", null, data, null) != null;
        }

        public bool InitSubsequent(string paymentId, int amount, Basket[] basket)
        {
            T data = this.CreateModelInstance();
            data.Id = paymentId;
            data.Amount = amount;
            data.Basket = basket;

            return this.Execute<Model.Transaction>(paymentId, "subsequent", null, data, null) != null;
        }

        public bool SetShippingInformation(string paymentId, ShippingInformation data)
        {
            return this.Execute<Model.Transaction>(paymentId, "shippingInformation", null, data, null) != null;
        }

        public bool UpdateSubscription(string paymentId, string purpose)
        {
            T data = this.CreateModelInstance();
            data.Id = paymentId;
            data.Subscription = new Subscription { Purpose = purpose };

            return this.Execute<Model.Transaction>(paymentId, "subscription", null, data, null) != null;
        }

        public override void RegisterEvents()
        {
            Context.EventDispatcher.RegisterForEvent(GetType().Name, GetMetaData().ProductResource, Events.TypeChanged, this.OnChangedEvent);
        }

        private T CreateModelInstance()
        {
            // T obj = default(T);
            return Activator.CreateInstance<T>();
        }

        private void OnChangedEvent(object obj)
        {
            var t = (Event<T[]>)obj;

            // Get the current data of the changed payment transaction
            for (int key = 0; key < t.Data.Length; ++key)
            {
                t.Data[key] = this.Get(t.Data[key].Id);
            }

            // Handle event
            this.PaymentEvent(this, new PaymentEventEventArgs<T> { SecucardEvent = t });
        }
    }
}