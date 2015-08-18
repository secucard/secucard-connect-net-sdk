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

namespace Secucard.Connect.Net.Stomp
{
    public class StompDestination
    {
        public string Command { get; set; } // standard api command like defined by constants above
        public string Action { get; set; }

        public string Product { get; set; }
        public string Resource { get; set; }

        public StompDestination(string product, string resource)
        {
            Product = product;
            Resource = resource;
        }

        public override string ToString()
        {
            string destination = "/exchange/connect.api/" + "api:" + Command;

            if (!string.IsNullOrWhiteSpace(Product))
            {
                destination += Product + "." + Resource;
            }

            if (Action != null)
            {
                destination += "." + Action;
            }

            return destination;
        }
    }
}