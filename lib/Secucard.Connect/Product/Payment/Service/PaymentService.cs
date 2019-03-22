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

            return this.ExecuteToBool(paymentId, "cancel", null, data, null);
        }
        
        public bool Capture(string paymentId)
        {
            return this.Execute<T>(paymentId, "capture", null, null, null) != null;
        }

        public bool UpdateBasket(string paymentId, Basket[] basket)
        {
            T data = this.CreateModelInstance();
            data.Id = paymentId;
            data.Basket = basket;

            return this.Update<T>(paymentId, "basket", null, data, null) != null;
        }

        public bool ReverseAccrual(string paymentId)
        {
            T data = this.CreateModelInstance();
            data.Id = paymentId;
            data.Accrual = false;

            return this.Update<T>(paymentId, "accrual", null, data, null) != null;
        }

        public bool SetShippingInformation(string paymentId, ShippingInformation data)
        {
            return this.UpdateToBool(paymentId, "shippingInformation", null, data, null);
        }

        public bool AssignExternalInvoicePdf(string paymentId, string documentUploadId, bool updateExisting = false, string name = "")
        {
            var data = new AssignExternalInvoicePdfRequest();
            data.updateExisting = updateExisting;
            data.name = name;

            return this.ExecuteToBool(paymentId, "assignExternalInvoicePdf", documentUploadId, data, null);
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