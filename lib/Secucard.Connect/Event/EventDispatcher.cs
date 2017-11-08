namespace Secucard.Connect.Event
{
    using System;
    using System.Collections.Generic;
    using Secucard.Connect.Client;
    using Secucard.Connect.Net.Stomp;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.General.Model;
    using Secucard.Connect.Product.Payment.Model;

    internal class EventDispatcher
    {
        private readonly Dictionary<string, Dictionary<string, Action<object>>> _eventKeySubscriptions;

        internal EventDispatcher()
        {
            _eventKeySubscriptions = new Dictionary<string, Dictionary<string, Action<object>>>();
        }

        /// <summary>
        ///     Register for events. Who am I (servicename), What am I interested in (target.type)
        /// </summary>
        internal void RegisterForEvent(string servicename, string target, string type, Action<object> methodToCall)
        {
            var eventKey = target + "." + type;
            Dictionary<string, Action<object>> subscribers;
            // Check if there is subscriber to that event. If not create it
            if (!_eventKeySubscriptions.TryGetValue(eventKey, out subscribers))
            {
                subscribers = new Dictionary<string, Action<object>>();
                _eventKeySubscriptions.Add(target + "." + type, subscribers);
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
            if (!_eventKeySubscriptions.TryGetValue(eventKey, out subscribers)) return;

            // call registered delegates of services.
            foreach (var ele in subscribers)
            {
                try
                {
                    ele.Value?.Invoke(serverEvent);
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

        internal void MessageArrivedEvent(object sender, string json)
        {
            dynamic evnt = null;
            var dict = JsonSerializer.DeserializeToDictionary(json);

            // Check if it is an event.pushes message
            if (dict.ContainsKey("object") && dict.ContainsKey("target") && "event.pushes".Equals(dict["object"]))
            {
                switch ((string)dict["target"])
                {
                    case "payment.secupaycreditcards":
                        evnt = JsonSerializer.DeserializeJson<Event<SecupayCreditcard[]>>(json);
                        break;
                    case "payment.secupaydebits":
                        evnt = JsonSerializer.DeserializeJson<Event<SecupayDebit[]>>(json);
                        break;
                    case "payment.secupayinvoices":
                        evnt = JsonSerializer.DeserializeJson<Event<SecupayInvoice[]>>(json);
                        break;
                    case "payment.secupayprepays":
                        evnt = JsonSerializer.DeserializeJson<Event<SecupayPrepay[]>>(json);
                        break;
                }

                if (evnt != null)
                {
                    OnNewEvent(evnt.Target, evnt.Type, evnt);
                }
            }
        }
    }
}
