namespace Secucard.Model.General
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Account : SecuObject
    {
        public override string ServiceResourceName { get { return "general.accounts"; } }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "role")]
        public string Role { get; set; }

        [DataMember(Name = "contact")]
        public Contact Contact { get; set; }

        [DataMember(Name = "assignment")]
        public List<Assignment> Assignment { get; set; }

    }
}
