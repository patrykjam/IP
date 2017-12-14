using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MoreLinq;
using WiFiMap.Helpers;
using WiFiMap.Models;

namespace WiFiMap.Controllers
{
    public class DataController : ApiController
    {
        private readonly WiFiDbModel _dbContext;

        public DataController()
        {
            _dbContext = new WiFiDbModel();
        }

        // GET /api/data
        public List<SentData> Get()
        {
            var list = _dbContext.ALL_DATA.ToList();
            list.RemoveAll(d => d.SSID.Contains("UPC Wi-Free"));
            var sentData = new List<SentData>();
            foreach (var bssid in list.Select(d => d.BSSID).Distinct())
            {
                var allDatasForBssid = list.FindAll(d => d.BSSID == bssid);
                var maxRssiData = allDatasForBssid.MaxBy(d => d.RSSI);
                var maxDist = allDatasForBssid.Max(d =>
                    DistanceCalc.DistanceBetweenPlaces(maxRssiData.LATITUDE, maxRssiData.LONGITUDE, d.LATITUDE,
                        d.LONGITUDE));
                if (maxDist > 100) maxDist = 50;
                sentData.Add(new SentData
                {
                    LATITUDE = maxRssiData.LATITUDE,
                    LONGITUDE = maxRssiData.LONGITUDE,
                    RSSI = maxRssiData.RSSI,
                    MAX_DISTANCE = maxDist > 0.00001 ? maxDist : 0.5,
                    SSID = maxRssiData.SSID,
                    EXTRA_INFO = maxRssiData.AUTH_TYPE,
                    FREE = !maxRssiData.AUTH_TYPE.Contains("WPA"),
                    BSSID = maxRssiData.BSSID
                });
            }
            return sentData;
        }

        [Route("api/data/free")]
        public List<SentData> GetFreeWiFis()
        {
            return Get().Where(data => data.FREE).ToList();
        }

    }
}
