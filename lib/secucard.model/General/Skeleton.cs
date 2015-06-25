namespace Secucard.Model.General
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Skeleton : SecuObject
    {

        [DataMember(Name = "a")]
        public string A;

        [DataMember(Name = "a")]
        public string B;

        [DataMember(Name = "a")]
        public string C;

        [DataMember(Name = "a")]
        public int Amount;

        [DataMember(Name = "a")]
        public string Picture;

        [DataMember(Name = "a")]
        public string Date;

        [DataMember(Name = "a")]
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
