using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace WiFiServer.Controllers
{
    public class TrackingController : ApiController
    {
        // POST /tracking
        public ActionResult Post([FromBody]Credentials data)
        {
            var d = data;
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }

    public class Credentials
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
