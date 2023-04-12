using System;
using Google.Protobuf;

namespace OneB
{
    public class DeleteInboxCommand<T> : BaseCommand<T> where T : IMessage<T>, new()
    {
        public DeleteInboxCommand(T input)
        {
            FunctionName = "MarkDeleteItem";
            Data = input;
            serviceName = Service.Inbox;
        }
    }
    public class DeleteInboxCommand : DeleteInboxCommand<InboxDeleteInput>
    {
        public DeleteInboxCommand(InboxDeleteInput input) : base(input)
        {

        }
    };
}
