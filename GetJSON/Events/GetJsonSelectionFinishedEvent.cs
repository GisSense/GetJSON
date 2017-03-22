using ArcGIS.Core.Events;
using ArcGIS.Desktop.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetJSON.Events
{
    public class GetJsonSelectionFinishedEvent : CompositePresentationEvent<GetJsonSelectionFinishedEventArgs>
    {
        public static SubscriptionToken Subscribe(Action<GetJsonSelectionFinishedEventArgs> action, bool keepSubscriberAlive = false)
        {
            return FrameworkApplication.EventAggregator.GetEvent<GetJsonSelectionFinishedEvent>().Register(action, keepSubscriberAlive);
        }
        public static void Unsubscribe(Action<GetJsonSelectionFinishedEventArgs> action)
        {
            FrameworkApplication.EventAggregator.GetEvent<GetJsonSelectionFinishedEvent>().Unregister(action);
        }

        public static void Unsubscribe(SubscriptionToken token)
        {
            FrameworkApplication.EventAggregator.GetEvent<GetJsonSelectionFinishedEvent>().Unregister(token);
        }

        internal static void Publish(GetJsonSelectionFinishedEventArgs activeMapViewEventArgs)
        {
            FrameworkApplication.EventAggregator.GetEvent<GetJsonSelectionFinishedEvent>().Broadcast(activeMapViewEventArgs);
        }
    }
}
