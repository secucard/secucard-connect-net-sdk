namespace Secucard.Model.Loyalty
{
    using System.Runtime.Serialization;
    using Secucard.Model.General;

    [DataContract]
    public class CardGroup : SecuObject
    {
        //public static final String OBJECT = "loyalty.cardgroups";
		        public override string SecuObjectName
        {
            get { return "loyalty.cardgroups"; }
        }

        [DataMember(Name = "displayName")]
        public string DisplayName;

        [DataMember(Name = "display_name_raw")]
        public string DisplayNameRaw;

        [DataMember(Name = "stock_warn_limit")]
        public int StockWarnLimit;

        [DataMember(Name = "merchant")]
        public Merchant Merchant;

        [DataMember(Name = "picture")]
        public string Picture;

        public MediaResource pictureObject;


        //public void setPicture(String value)
        //{
        //    this.picture = value;
        //    pictureObject = MediaResource.create(picture);
        //}


    }
}
