using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object obj)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(obj.GetType());
                serializer.WriteObject(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        public static object ToInstance(this string xml, Type type)
        {
            var serializer = new DataContractJsonSerializer(type);
            MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            return serializer.ReadObject(memStream);
        }


    }
}
