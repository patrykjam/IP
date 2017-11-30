namespace WiFiMap.Models
{
    public class WIFI_DATA
    {
        public int ID { get; set; }

        public int WIFI_DATE { get; set; }

        public int WIFI_LOCATION { get; set; }

        public int DEVICE { get; set; }

        public int BSSID { get; set; }

        public int AUTH_TYPE { get; set; }

        public int RSSI { get; set; }

        public int SSID { get; set; }

        public virtual AUTH_TYPES AUTH_TYPES { get; set; }

        public virtual BSSIDS BSSIDS { get; set; }

        public virtual DATES DATES { get; set; }

        public virtual DEVICES DEVICES { get; set; }

        public virtual LOCATIONS LOCATIONS { get; set; }

        public virtual SSIDS SSIDS { get; set; }
    }
}
