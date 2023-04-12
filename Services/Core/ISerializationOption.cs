
using Google.Protobuf;

namespace OneB
{
    public interface ISerializationOption
    {
        string ContentType { get; }
        T Deserialize<T>(byte[] data) where T : IMessage<T>, new();
    }
}


