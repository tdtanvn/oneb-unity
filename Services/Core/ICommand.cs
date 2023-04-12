
namespace OneB
{
    public interface ICommand
    {
        Request GetRequest();
        Request GetBinRequest();
    }
}
