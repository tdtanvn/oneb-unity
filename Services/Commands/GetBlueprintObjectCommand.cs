using Google.Protobuf;

namespace OneB
{
    public class GetBlueprintObjectCommand<T> : BaseCommand<T> where T : IMessage<T>, new()
    {
        public GetBlueprintObjectCommand(string dataName)
        {
            this.FunctionName = $"Get{dataName}";
            serviceName = Service.Blueprint;
        }
    }
    public class GetBlueprintObjectCommand : GetBlueprintObjectCommand<Empty>
    {
        public GetBlueprintObjectCommand(string dataName) : base(dataName)
        {
        }

    }
}
