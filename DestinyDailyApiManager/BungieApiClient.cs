using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager
{
    public class BungieApiClient
    {
        public const string BungieBaseUri = "https://www.bungie.net/";
        public const string AccessTokenRequest = "Platform/App/GetAccessTokensFromCode/";
        public const string RefreshTokenRequest = "Platform/App/GetAccessTokensFromRefreshToken/";

        public const string AuthenticationCodeRequest = "https://www.bungie.net/en/Application/Authorize/11093";
        private const string ApiKey = "9681c0a6c9f44315bef80e15a4e3b469";

        private const int Success = 1;
        private string _authCode;
        private string _accessToken;
        private string _refreshToken;

        public HttpClient Client { get; set; }
        public BungieApiClient()
        {
            PrepareBungieRequests();
        }

        public void PrepareBungieRequests()
        {
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            Client = new HttpClient(handler);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
            Client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            Client.DefaultRequestHeaders.Add("X-API-Key", ApiKey);
        }
    }
}
