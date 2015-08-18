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

namespace Secucard.Connect.Product.Smart.Model
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;
    using Secucard.Connect.Product.Loyalty.Model;

    [DataContract]
    public class Checkin : SecuObject
    {
        [DataMember(Name = "account")]
        public Account Account { get; set; }

        public DateTime? Created { get; set; }

        [DataMember(Name = "customer")]
        public Customer Customer { get; set; }

        [DataMember(Name = "customerName")]
        public string CustomerName { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        private string _picture;

        [DataMember(Name = "picture")]
        public string Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                PictureObject = MediaResource.Create(value);
            }
        }

        [IgnoreDataMember]
        public MediaResource PictureObject { get; set; }

        public override string ToString()
        {
            return "Checkin{" +
                   "customerName='" + CustomerName + '\'' +
                   ", pictureUrl='" + Picture + '\'' +
                   ", created=" + Created +
                   "} " + base.ToString();
        }
    }
}