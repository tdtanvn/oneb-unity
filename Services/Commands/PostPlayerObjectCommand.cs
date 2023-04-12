using Google.Protobuf;

namespace OneB
{
    public class PostPlayerObjectCommand<T> : BaseCommand<T> where T : IMessage<T>, new()
    {
        public PostPlayerObjectCommand(string dataName, T data)
        {
            FunctionName = $"Update{dataName}";
            serviceName = Service.Player;
            Data = data;
        }
    }
}
