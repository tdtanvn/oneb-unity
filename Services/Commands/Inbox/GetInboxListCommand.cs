using System;
using Google.Protobuf;

namespace OneB
{

    public class GetInboxListCommand<T> : BaseCommand<T> where T : IMessage<T>, new()
    {
        public GetInboxListCommand()
        {
            FunctionName = "GetInfo";
            serviceName = Service.Inbox;
        }
    }
    public class GetInboxListCommand : GetInboxListCommand<Empty> { };
}

