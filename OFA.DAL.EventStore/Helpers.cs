using EventStore.ClientAPI;
using OFA.Common;
using OFA.Common.Messages.Events;

namespace OFA.DAL.EventStore
{
    public static partial class Helpers
    {
        public static EventData Payload(this IEvent obj)
        {
            string streamType = "default-unspecified";
            switch (obj.GetType().Name)
            {
                case "CustomerCreated":
                    streamType = "customer";
                    break;
                default: break;
            }

            return new EventData(obj.EventId, streamType, true, obj.ToBytes(), "{}".ToBytes());
        }
    }
}
