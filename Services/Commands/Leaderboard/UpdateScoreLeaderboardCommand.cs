using System;
using Google.Protobuf;

namespace OneB
{
    public class UpdateScoreLeaderboardCommand<T> : BaseCommand<T> where T : IMessage<T>, new()
    {
        public UpdateScoreLeaderboardCommand(T input)
        {
            FunctionName = "UpdateScore";
            Data = input;
            serviceName = Service.Leaderboard;
        }
    }
    public class UpdateScoreLeaderboardCommand : UpdateScoreLeaderboardCommand<UpdateScoreLeaderboardInput>
    {
        public UpdateScoreLeaderboardCommand(UpdateScoreLeaderboardInput input) : base(input)
        {

        }
    };
}
