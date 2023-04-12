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
        public Request GetRequest()
        {
            JsonFormatter formater = new JsonFormatter(new JsonFormatter.Settings(false));
            var jsonString = Data is IMessage ? formater.Format(Data as IMessage) : JsonUtility.ToJson(Data);
            return new() { Service = serviceName.ToString(), Param = FunctionName, RequestVerb = verb, Body = Encoding.UTF8.GetBytes(jsonString) };
        }

        public virtual Request GetBinRequest()
        {
            var protoMessage = new ProtoMessage { FunctionName = this.FunctionName, Service = serviceName, Data = Data != null ? Data.ToByteString() : ByteString.Empty, Namespace = ns };
            Debug.Log(protoMessage);
            return new() { RequestVerb = RequestVerb.POST, Service = "bin", Body = protoMessage.ToByteArray() };
        }
    }
}
