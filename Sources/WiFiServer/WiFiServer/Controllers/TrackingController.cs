using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using WiFiServer.Helpers;
using WiFiServer.Models;

namespace WiFiServer.Controllers
{
    public class TrackingController : ApiController
    {

        // POST /tracking
        public ActionResult Post([FromBody]JsonData data)
        {
            var dbHelper = new DbHelper();
            
            var device = dbHelper.GetDevice(data.DeviceId);

            if (device.BLOCKED)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            var location = dbHelper.GetLocation(data.LocationData);

            foreach (var result in data.ResultsData)
            {
                var bssid = dbHelper.GetBSSID(result.BSSID, int.Parse(result.Frequency));
                if (bssid.BLOCKED)
                {
                    continue;
                }
                var ssid = dbHelper.GetSSID(result.SSSID, result.BSSID, int.Parse(result.Frequency));
                if (ssid.BLOCKED)
                {
                    continue;
                }
                var wifiDate = dbHelper.GetDate(result.WiFiDate);
                var authType = dbHelper.GetAuthType(result.AuthType);
                var rssi = int.Parse(result.RSSI);
                dbHelper.InsertWiFiData(wifiDate.ID, location.ID, device.ID, bssid.ID, authType.ID, rssi, ssid.ID);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
