namespace Secucard.Connect.Product.General.Model
{
    using System.Runtime.Serialization;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class ResultClass : SecuObject
    {

        public override string ServiceResourceName
        {
            get { return "general.result"; }
        }

        [DataMember(Name = "result")]
        public bool? Result { get; set; }


    }
}