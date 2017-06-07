 namespace Secucard.Connect.Product.Common.Model
{
    using System;

    public class ProductException : Exception
    {
        public Status Status { get; set; }

        public ProductException(string message) : base(message)
        {
            
        }
    }
}
