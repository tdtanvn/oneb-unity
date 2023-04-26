
namespace OneB
{
    public interface ICommand
    {
        void SetDebugLogEnabled(bool enable);
        Request GetRequest();
    }
}
