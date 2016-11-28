using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyApiManager.Models.Advisors;
using DestinyDailyApiManager.Models.Manifest;
using DestinyDailyApiManager.Models.OldAdvisors;
using Newtonsoft.Json;

namespace DestinyDailyApiManager
{
    public static class BungieApi
    {
        public static AdvisorsEndpoint GetAdvisors()
        {
            var advisorv2 = MakeBungieRequest("Advisors/V2/").Replace("prisonofelders-playlist", "prisonofeldersplaylist");
            return JsonConvert.DeserializeObject<AdvisorsEndpoint>(advisorv2);
        }

        public static OlderAdvisorsEndpoint GetOldAdvisors()
        {
            var advisorv2 = MakeBungieRequest("Advisors/");
            return JsonConvert.DeserializeObject<DestinyDailyApiManager.Models.OldAdvisors.OlderAdvisorsEndpoint>(advisorv2);
        }

        public static Manifest GetManifestUrl()
        {
            var manifestText =  MakeBungieRequest("Manifest/");
            return JsonConvert.DeserializeObject<Manifest>(manifestText);
        }

        public static OlderAdvisorsEndpoint GetVendorMetaData(string vendorId)
        {
            var vendorResponse = MakeBungieRequest($"Vendors/{vendorId}/Metadata/");
            return JsonConvert.DeserializeObject<OlderAdvisorsEndpoint>(vendorResponse);
        }

        private static string MakeBungieRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(String.Format("https://www.bungie.net/Platform/Destiny/{0}", url.Trim()));

            request.Headers.Add("X-API-Key", @"C95158E80AD34135845C7D6118F2A7B2");
            request.Headers.Add("X-csrf", @"6645961750234506012");
            request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            request.Accept = "*/*";
            request.KeepAlive = true;
            request.AutomaticDecompression = DecompressionMethods.GZip;
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            request.CachePolicy = noCachePolicy;

            var response = (HttpWebResponse) request.GetResponse();
            var responseHeader = response.Headers.ToString();

            // Open the stream using a StreamReader for easy access.
            var reader = new StreamReader(response.GetResponseStream());
            var responseBody = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            response.Close();

            return responseBody;
        }
    }
}
