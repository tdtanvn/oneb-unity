using Google.Protobuf;

namespace OneB
{
    public class GetMyRankLeaderboardCommand<T> : BaseCommand<T> where T : IMessage<T>, new()
    {
        public GetMyRankLeaderboardCommand(T input)
        {
            FunctionName = "GetMyRank";
            Data = input;
            serviceName = Service.Leaderboard;
        }
    }
    public class GetMyRankLeaderboardCommand : GetMyRankLeaderboardCommand<GetMyRankLeaderboardInput>
    {
        public GetMyRankLeaderboardCommand(GetMyRankLeaderboardInput input) : base(input)
        {

        }
    };
}
