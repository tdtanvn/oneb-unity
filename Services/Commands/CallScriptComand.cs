using System;
using Google.Protobuf;

namespace OneB
{
    public class CallGameScriptCommand<T> : BaseCommand<T> where T : IMessage<T>, new()
    {
        public CallGameScriptCommand(string scriptName, string functionName, T input)
        {
            FunctionName = functionName;
            ns = scriptName;
            Data = input;
            serviceName = Service.Gamescript;
        }
    }
    public class CallGameScriptCommand : CallGameScriptCommand<Empty>
    {
        public CallGameScriptCommand(string scriptName, string functionName) : base(scriptName, functionName, null)
        {

        }
    }
}
