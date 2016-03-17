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

namespace Secucard.Connect.Event
{
    using System;
    using System.Collections.Generic;
    using Secucard.Connect.Client;
    using Secucard.Connect.Net.Stomp;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.General.Model;

    internal class EventDispatcher
    {
        private readonly Dictionary<string, Dictionary<string, Action<object>>> EventKeySubscriptions;

        internal EventDispatcher()
        {
            EventKeySubscriptions = new Dictionary<string, Dictionary<string, Action<object>>>();
        }

        /// <summary>
        ///     Register for events. Who am I (servicename), What am I interested in (target.type)
        /// </summary>
        internal void RegisterForEvent(string servicename, string target, string type, Action<object> methodToCall)
        {
            var eventKey = target + "." + type;
            Dictionary<string, Action<object>> subscribers;
            // Check if there is subscriber to that event. If not create it
            if (!EventKeySubscriptions.TryGetValue(eventKey, out subscribers))
            {
                subscribers = new Dictionary<string, Action<object>>();
                EventKeySubscriptions.Add(target + "." + type, subscribers);
            }

            // Add or replace the call for the service
            if (subscribers.ContainsKey(servicename))
                subscribers[servicename] = methodToCall;
            else
                subscribers.Add(servicename, methodToCall);
        }

        private void OnNewEvent<T>(string target, string type, T serverEvent)
        {
            // check who ist interested in this event
            var eventKey = target + "." + type;
            Dictionary<string, Action<object>> subscribers;

            // Check if there is subscriber to that event. If not create it
            if (!EventKeySubscriptions.TryGetValue(eventKey, out subscribers)) return;

            // call registered delegates of services.
            foreach (var ele in subscribers)
            {
                try
                {
                    if (ele.Value != null) ele.Value(serverEvent);
                }
                catch (Exception ex)
                {
                    // Don't stop calling all subscribers
                    SecucardTrace.Exception(ex);
                }
            }
        }

        internal void StompMessageArrivedEvent(object sender, StompEventArrivedEventArgs args)
        {
            var body = args.Body;
            var dict = JsonSerializer.DeserializeToDictionary(body);

            // Check if it is an event.pushs message
            if (dict.ContainsKey("object") && (string) dict["object"] == "event.pushs")
            {
                if (dict.ContainsKey("type") && (string) dict["type"] == "display")
                {
                    // Unclear: why type display for Notification
                    var e = JsonSerializer.DeserializeJson<Event<Notification>>(body);
                    OnNewEvent(e.Target, e.Type, e);
                }
                else
                {
                    // TODO:
                    throw new Exception();
                }
            }
        }
    }
}