
using System;
using Google.Protobuf;
using UnityEngine;
namespace OneB
{
    public class ProtoSerializationOption : ISerializationOption
    {
        public string ContentType => "application/octet-stream";

        public T Deserialize<T>(byte[] data) where T : IMessage<T>, new()
        {
            try
            {
                if( data == null)
                {
                    return default;
                }
                T message = Activator.CreateInstance<T>();
                message.MergeFrom(data);
                return message;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Could not parse response {data}. {ex.Message}");
                return default;
            }
        }
    }
}

