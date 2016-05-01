using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FTPLibrary
{
    //class serializies objects into a memory stream
    public static class PayLoad
    {
        public static byte[] ObjectToByte(this object obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
                serializer.WriteObject(memoryStream, obj);
                memoryStream.Position = 0;
                return memoryStream.ToArray();
            }

        }

        public static object ByteStreamToObject(this byte[] packet, Type t)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(packet, 0, packet.Length);
                stream.Position = 0;
                DataContractSerializer deserializer = new DataContractSerializer(t);

                return deserializer.ReadObject(stream);
            }
        }
    }
}
