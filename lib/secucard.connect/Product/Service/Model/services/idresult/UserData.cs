namespace Secucard.Connect.Product.Service.Model.services.idresult
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UserData
    {
        [DataMember(Name = "dob")]
        public ValueClass DateOfBirth { get; set; }

        [DataMember(Name = "forename")]
        public ValueClass Forename { get; set; }

        [DataMember(Name = "surname")]
        public ValueClass Surname { get; set; }

        [DataMember(Name = "address")]
        public Address Address { get; set; }

        [DataMember(Name = "birthplace")]
        public ValueClass Birthplace { get; set; }

        [DataMember(Name = "nationality")]
        public ValueClass Nationality { get; set; }

        [DataMember(Name = "gender")]
        public ValueClass Gender { get; set; }

        public override string ToString()
        {
            return "UserData{" +
                   "birthday=" + DateOfBirth +
                   ", firstname=" + Forename +
                   ", lastname=" + Surname +
                   ", address=" + Address +
                   ", birthplace=" + Birthplace +
                   ", nationality=" + Nationality +
                   ", gender=" + Gender +
                   '}';
        }
    }
}