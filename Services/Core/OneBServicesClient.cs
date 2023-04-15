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
        private string AccessToken;
        private ISerializationOption serializationOption = new ProtoSerializationOption();
        public string GameId;
        public GameEnvironment Environment = GameEnvironment.DEVELOPMENT;
        public string GameVersion = "";
        public ApiType ApiType = ApiType.BINARY;
        public async Task<TResultType> Send<TResultType>(ICommand command) where TResultType : IMessage<TResultType>, new()
        {
            Request request = command.GetRequest();
            string uri = BaseURL[Environment.ToString()] + "/" + request.Service;

            UnityWebRequest unityWebRequest = new UnityWebRequest(uri, request.RequestVerb.ToString());
            if (request.RequestVerb == RequestVerb.POST)
            {
                unityWebRequest.uploadHandler = new UploadHandlerRaw(request.Body);
            }
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
            unityWebRequest.SetRequestHeader("Content-Type", serializationOption.ContentType);
            unityWebRequest.SetRequestHeader("Authorization", $"Bearer {AccessToken}");
            unityWebRequest.SetRequestHeader("X-API-Version", X_API_VERSION);
            var operation = unityWebRequest.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (unityWebRequest.result == UnityWebRequest.Result.Success)
            {
                return serializationOption.Deserialize<TResultType>(unityWebRequest.downloadHandler.data);
            }
            var error = unityWebRequest.downloadHandler.text;
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

