using Google.Protobuf;

namespace OneB
{
    public class ClaimInboxCommand<T> : BaseCommand<T> where T : IMessage<T>, new()
    {
        public ClaimInboxCommand(T input)
        {
            FunctionName = "ClaimItem";
            Data = input;
            serviceName = Service.Inbox;
        }
    }
    public class ClaimInboxCommand : ClaimInboxCommand<InboxClaimInput>
    {
        public ClaimInboxCommand(InboxClaimInput input) : base(input)
        {

        }
    };
}
