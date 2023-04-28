using Google.Protobuf;

namespace OneB
{
    public class GetAuthTokenCommand<T> : BaseCommand<T> where T : IMessage<T>, new()
    {
        public GetAuthTokenCommand(T data = default(T))
        {
            Data = data;
            FunctionName = "token";
        }
    }
}

