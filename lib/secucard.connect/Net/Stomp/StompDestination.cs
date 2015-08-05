namespace Secucard.Connect.Net.Stomp
{
    public class StompDestination
    {
        public string Command;  // standard api command like defined by constants above
        public string Action;
        public string Product;
        public string Resource;
        public object Obj;

        public StompDestination(string product, string resource)
        {
            Product = product;
            Resource = resource;
        }

        public override string ToString()
        {
            string dest = "/exchange/connect.api/" + "api:" + Command;

            if (!string.IsNullOrWhiteSpace(Product))
            {
                dest +=  Product + "." + Resource;
            }

            if (Action != null)
            {
                dest += "." + Action;
            }

            return dest;
        }
    }
}
