using EventStore.ClientAPI;
using Newtonsoft.Json;
using OFA.Repayment.WM.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Repayment.WM
{
    public static class Helpers
    {
        public static string ExceptionTemplate
            => "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception} {Properties:j}";
        public static byte[] ToBytes(this object obj)
            => obj != null ?
            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)) :
            new byte[0];
        public static TEvent FromBytes<TEvent>(this byte[] obj)
            => JsonConvert.DeserializeObject<TEvent>(Encoding.UTF8.GetString(obj));
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
