using Google.Protobuf;

namespace OneB
{
    public class GetTopScoreLeaderboardCommand<T> : BaseCommand<T> where T : IMessage<T>, new()
    {
        public GetTopScoreLeaderboardCommand(T input)
        {
            FunctionName = "GetTopScore";
            Data = input;
            serviceName = Service.Leaderboard;
        }
    }
    public class GetTopScoreLeaderboardCommand : GetTopScoreLeaderboardCommand<GetTopLeaderboardInput>
    {
        public GetTopScoreLeaderboardCommand(GetTopLeaderboardInput input) : base(input)
        {

        }
    };
}
