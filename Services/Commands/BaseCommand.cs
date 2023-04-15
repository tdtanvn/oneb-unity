using System.Text;
using Google.Protobuf;
using UnityEngine;

namespace OneB
{
    public abstract class BaseCommand<T> : ICommand where T : IMessage<T>, new()
    {
        protected Service serviceName;

        protected string ns = "";
        protected RequestVerb verb = RequestVerb.POST;
        public T Data;
        public string FunctionName;

        public virtual Request GetRequest()
        {
            var protoMessage = new ProtoMessage { FunctionName = this.FunctionName, Service = serviceName, Data = Data != null ? Data.ToByteString() : ByteString.Empty, Namespace = ns };
            Debug.Log(protoMessage);
            return new() { RequestVerb = RequestVerb.POST, Service = "bin", Body = protoMessage.ToByteArray() };
        }
    }
}
