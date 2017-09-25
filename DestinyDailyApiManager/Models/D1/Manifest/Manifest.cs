using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.Manifest
{
    public class MobileGearAssetDataBas
    {
        public int version { get; set; }
        public string path { get; set; }
    }

    public class MobileWorldContentPaths
    {
        public string en { get; set; }
        public string fr { get; set; }
        public string es { get; set; }
        public string de { get; set; }
        public string it { get; set; }
        public string ja { get; set; }
    }

    public class MobileGearCDN
    {
        public string Geometry { get; set; }
        public string Texture { get; set; }
        public string PlateRegion { get; set; }
        public string Gear { get; set; }
        public string Shader { get; set; }
    }

    public class Response
    {
        public string version { get; set; }
        public string mobileAssetContentPath { get; set; }
        public List<MobileGearAssetDataBas> mobileGearAssetDataBases { get; set; }
        public MobileWorldContentPaths mobileWorldContentPaths { get; set; }
        public MobileGearCDN mobileGearCDN { get; set; }
    }

    public class MessageData
    {
    }

    public class Manifest
    {
        public Response Response { get; set; }
        public int ErrorCode { get; set; }
        public int ThrottleSeconds { get; set; }
        public string ErrorStatus { get; set; }
        public string Message { get; set; }
        public MessageData MessageData { get; set; }
    }
}
