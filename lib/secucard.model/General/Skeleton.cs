namespace Secucard.Model.General
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Skeleton : SecuObject
    {

        [DataMember(Name = "a")]
        public string A;

        [DataMember(Name = "b")]
        public string B;

        [DataMember(Name = "c")]
        public string C;

        [DataMember(Name = "amount")]
        public int Amount;

        [DataMember(Name = "date")]
        public string Date;

        [DataMember(Name = "picture")]
        public string Picture;

        [DataMember(Name = "type")]
        public string Type;

        [DataMember(Name = "location")]
        public Location Location;

        [DataMember(Name = "skeleton")]
        public Skeleton SkeletonObj;

        [DataMember(Name = "skeleton_list")]
        public List<Skeleton> SkeletonList;


        public override string ToString()
        {
            return "Skeleton{" +
                   ", id='" + Id + '\'' +
                   ", a='" + A + '\'' +
                   ", b='" + B + '\'' +
                   ", c='" + C + '\'' +
                   ", amount=" + Amount +
                   ", picture='" + Picture + '\'' +
                   ", date='" + Date + '\'' +
                   ", type='" + Type + '\'' +
                   ", location=" + Location +
                   ", skeleton=" + SkeletonObj +
                   ", skeleton_list=" + SkeletonList +
                   '}';
        }

        public override string ServiceResourceName
        {
            get { return "general.skeletons"; }
        }
    }
}
