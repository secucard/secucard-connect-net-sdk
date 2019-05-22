namespace Secucard.Connect.Product.Payment.Model
{
    using System.Runtime.Serialization;
    using Common.Model;

    [DataContract]
    public class TransactionList : SecuObject
    {
        public const string ItemTypeTransactionPayout = "transaction_payout";

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "total")]
        public int? Total { get; set; }

        [DataMember(Name = "item_type")]
        public string ItemType { get; set; }

        [DataMember(Name = "transaction_hash")]
        public string TransactionHash { get; set; }

        [DataMember(Name = "transaction_id")]
        public string TransactionId { get; set; }

        [DataMember(Name = "container_id")]
        public string ContainerId { get; set; }

        [DataMember(Name = "reference_id")]
        public string ReferenceId { get; set; }

        public override string ToString()
        {
            return "TransactionList{" +
                   "Name=" + this.Name +
                   "Total=" + this.Total +
                   "ItemType=" + this.ItemType +
                   "TransactionHash=" + this.TransactionHash +
                   "TransactionId=" + this.TransactionId +
                   "ContainerId=" + this.ContainerId +
                   "ReferenceId=" + this.ReferenceId +
                   "} " + base.ToString();
        }
    }
}
