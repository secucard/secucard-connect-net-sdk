namespace Secucard.Connect.Net.Stomp
{
    public class StompDestination
    {
        public string Command { get; set; } // standard api command like defined by constants above
        public string Action { get; set; }

        public string Product { get; set; }
        public string Resource { get; set; }

        public StompDestination(string product, string resource)
        {
            Product = product;
            Resource = resource;
        }

        public override string ToString()
        {
            string destination = "/exchange/connect.api/" + "api:" + Command;

            if (!string.IsNullOrWhiteSpace(Product))
            {
                destination += Product + "." + Resource;
            }

            if (Action != null)
            {
                destination += "." + Action;
            }

            return destination;
        }
    }
}