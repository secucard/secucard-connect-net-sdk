/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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