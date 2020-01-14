using Newtonsoft.Json;
using OFA.Common.Messages.Events;
using System.Text;

namespace OFA.Common
{
    public static partial class Helpers
    {
        public static string ExceptionTemplate
            => "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception} {Properties:j}";
        public static byte[] ToBytes(this object obj)
            => obj != null ?
            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)) :
            new byte[0];
        public static TEvent FromBytes<TEvent>(this byte[] obj)
            => JsonConvert.DeserializeObject<TEvent>(Encoding.UTF8.GetString(obj));
    }
}
