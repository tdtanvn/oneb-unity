using Google.Protobuf;

namespace OneB
{

    public class GetPlayerObjectCommand<T> : BaseCommand<T> where T : IMessage<T>, new()
    {
        public GetPlayerObjectCommand(string dataName)
        {
            FunctionName = $"Get{dataName}";
            serviceName = Service.Player;
        }
    }
    public class GetPlayerObjectCommand : GetPlayerObjectCommand<Empty>
    {
        public GetPlayerObjectCommand(string dataName) : base(dataName) { }
    }
}
