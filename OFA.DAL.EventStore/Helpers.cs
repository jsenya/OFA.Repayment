using EventStore.ClientAPI;
using OFA.Common;
using OFA.Common.Messages.Events;

namespace OFA.DAL.EventStore
{
    public static partial class Helpers
    {
        public static EventData Payload(this IEvent obj)
        {
            string streamType = obj.GetType().Name;

            return new EventData(obj.EventId, streamType, true, obj.ToBytes(), "{}".ToBytes());
        }
    }
}
