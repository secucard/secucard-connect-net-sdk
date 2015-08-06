namespace Secucard.Connect.Product.General.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Geometry
    {
        [DataMember(Name = "lat")]
        public double Lat { get; set; }

        [DataMember(Name = "lon")]
        public double Lon { get; set; }
    }
}