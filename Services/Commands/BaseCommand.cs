using System.Text;
using Google.Protobuf;
using UnityEngine;

namespace OneB
{
    public abstract class BaseCommand<T> : ICommand where T : IMessage<T>, new()
    {
        private bool isDebug = false;
        protected Service serviceName;

        protected string ns = "";
        protected RequestVerb verb = RequestVerb.POST;
        public T Data;
        public string FunctionName;

        public void SetDebugLogEnabled(bool enable)
        {
            isDebug = enable;
        }
        public virtual Request GetRequest()
        {
            var protoMessage = new ProtoMessage
            {
                FunctionName = Utils.GetSHA256Hash(this.FunctionName),
                Service = serviceName,
                Data = Data != null ? Data.ToByteString() : ByteString.Empty,
                Namespace = !string.IsNullOrEmpty(ns) ? Utils.GetSHA256Hash(this.ns) : ns
            };
            if (isDebug)
            {
                Debug.LogFormat("Service: '{0}', function: '{1}', data: '{2}'", serviceName, FunctionName, Data);
            }
            return new() { RequestVerb = RequestVerb.POST, Body = protoMessage.ToByteArray() };
        }
    }
}
