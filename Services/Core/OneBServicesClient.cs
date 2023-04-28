using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Google.Protobuf;
using System;

namespace OneB
{
    public class OneBServicesClient
    {
        const string X_API_VERSION = "1";
        private Dictionary<string, string> BaseURL = new Dictionary<string, string>
                                                    {
                                                        {"LOCAL", "http://localhost:3000" },
                                                        {"DEVELOPMENT", "https://dev.api.oneb.tech" },
                                                        {"UAT", "https://uat.api.oneb.tech" },
                                                        {"PRODUCTION", "https://prod.api.oneb.tech" }
                                                    };
        public string AccessToken;
        private ISerializationOption serializationOption = new ProtoSerializationOption();
        public string GameId;
        public GameEnvironment Environment = GameEnvironment.DEVELOPMENT;
        public string GameVersion = "";
        public ApiType ApiType = ApiType.BINARY;

        public bool DebugLogEnabled = false;
        public async Task<TResultType> Send<TResultType>(ICommand command) where TResultType : IMessage<TResultType>, new()
        {
            command.SetDebugLogEnabled(DebugLogEnabled);
            Request request = command.GetRequest();
            string uri = BaseURL[Environment.ToString()] + "/bin/";
            if(string.IsNullOrEmpty(this.AccessToken))
            {
                uri += "p";
            }
            UnityWebRequest unityWebRequest = new UnityWebRequest(uri, request.RequestVerb.ToString());
            if (request.RequestVerb == RequestVerb.POST)
            {
                unityWebRequest.uploadHandler = new UploadHandlerRaw(request.Body);
            }
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
            unityWebRequest.SetRequestHeader("Content-Type", serializationOption.ContentType);
            unityWebRequest.SetRequestHeader("Authorization", $"Bearer {AccessToken}");
            unityWebRequest.SetRequestHeader("X-API-Version", X_API_VERSION);
            unityWebRequest.SetRequestHeader("gameId", GameId);
            unityWebRequest.SetRequestHeader("gameVer", GameVersion);
            var operation = unityWebRequest.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }
            if (unityWebRequest.result == UnityWebRequest.Result.Success)
            {
                var data = unityWebRequest.downloadHandler.data;
                unityWebRequest.Dispose();
                var result = serializationOption.Deserialize<TResultType>(data);
                if (DebugLogEnabled)
                {
                    Debug.LogFormat("Response: {0}", result);
                }
                return result;
            }
            var error = unityWebRequest.downloadHandler.text;
            unityWebRequest.Dispose();
            throw new Exception(error);

        }
        public async Task<AuthResponse> Login(string playerId, string secretKey, string playerName = "", string country = "")
        {
            var authLogin = new AuthLogin { PlayerName = playerName, PlayerId = playerId, SecretKey = secretKey, GameVersion = GameVersion, Country = country, GameId = GameId };
            var authInfo = await Send<AuthResponse>(new GetAuthTokenCommand<AuthLogin>(authLogin));
            AccessToken = authInfo.AccessToken;
            return authInfo;
        }
    }
}

