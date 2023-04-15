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
        public override Request GetRequest()
        {
            var request = base.GetRequest();
            request.Service = "bin/token";
            return request;
        }
    }
}

