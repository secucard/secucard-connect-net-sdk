/*
 * Copyright (c) 2014 secucard AG. All rights reserved
 */
namespace Secucard.Model.General.Components
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class AddressComponent
    {

        [DataMember(Name = "long_name")]
        public string LongName { get; set; }

        [DataMember(Name = "short_name")]
        private string ShortName { get; set; }

        [DataMember(Name = "types")]
        private List<string> Types { get; set; }
    }
}
