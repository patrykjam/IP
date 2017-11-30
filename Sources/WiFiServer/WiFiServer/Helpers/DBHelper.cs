using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WiFiServer.Models;

namespace WiFiServer.Helpers
{
    public class DbHelper
    {
        private readonly WiFiDbModel _dbContext;
        private const double Tolerance = 0.00000001;

        public DbHelper()
        {
            _dbContext = new WiFiDbModel();
        }

        public DEVICES GetDevice(string dataDeviceId)
        {
            var devices = _dbContext.DEVICES;
            var device = devices.FirstOrDefault(dev => dev.DEVICE_ID == dataDeviceId);

            if (device != null) return device;

            devices.Add(new DEVICES { DEVICE_ID = dataDeviceId, BLOCKED = false });

            _dbContext.SaveChanges();

            return devices.FirstOrDefault(dev => dev.DEVICE_ID == dataDeviceId);
        }

        public LOCATIONS GetLocation(Location dataLocation)
        {
            var locations = _dbContext.LOCATIONS;
            var location = locations
                .FirstOrDefault(l => Math.Abs(l.LATITUDE - dataLocation.Latitude) < Tolerance
                                  && Math.Abs(l.LONGITUDE - dataLocation.Longitude) < Tolerance);

            if (location != null) return location;

            locations.Add(new LOCATIONS { LATITUDE = dataLocation.Latitude, LONGITUDE = dataLocation.Longitude });

            _dbContext.SaveChanges();

            return locations
                .FirstOrDefault(l => Math.Abs(l.LATITUDE - dataLocation.Latitude) < Tolerance
                                     && Math.Abs(l.LONGITUDE - dataLocation.Longitude) < Tolerance);

        }

        public SSIDS GetSSID(string SSID, string BSSID, int frequency)
        {
            var ssids = _dbContext.SSIDS;
            var bssid = GetBSSID(BSSID, frequency);
            var ssid = ssids
                .FirstOrDefault(s => s.SSID == SSID && s.BSSID == bssid.ID);

            if (ssid != null) return ssid;

            ssids.Add(new SSIDS { SSID = SSID, BSSID = bssid.ID, BLOCKED = false });

            _dbContext.SaveChanges();

            return ssids
                .FirstOrDefault(s => s.SSID == SSID && s.BSSID == bssid.ID);
        }

        public BSSIDS GetBSSID(string BSSID, int frequency)
        {
            var bssids = _dbContext.BSSIDS;
            var bssid = bssids
                .FirstOrDefault(b => b.BSSID == BSSID);

            if (bssid != null) return bssid;

            bssids.Add(new BSSIDS { BSSID = BSSID, BLOCKED = false, IS_5_GHz = frequency > 3500 });

            _dbContext.SaveChanges();

            return bssids
                .FirstOrDefault(b => b.BSSID == BSSID);
        }

        public AUTH_TYPES GetAuthType(string authType)
        {
            var auths = _dbContext.AUTH_TYPES;
            var auth = auths
                .FirstOrDefault(a => a.AUTH_TYPE == authType);

            if (auth != null) return auth;

            auths.Add(new AUTH_TYPES { AUTH_TYPE = authType });

            _dbContext.SaveChanges();

            return auths
                .FirstOrDefault(a => a.AUTH_TYPE == authType);
        }

        public DATES GetDate(string dateData)
        {
            var jsonDate = DateTime.ParseExact(dateData, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            var dates = _dbContext.DATES;
            var date = dates
                .FirstOrDefault(d => d.WIFI_DATE.Value == jsonDate);

            if (date != null) return date;

            dates.Add(new DATES { WIFI_DATE = jsonDate });

            _dbContext.SaveChanges();

            return dates
                .FirstOrDefault(d => d.WIFI_DATE.Value == jsonDate);
        }

        public void InsertWiFiData(int date, int location, int device, int bssid, int authType, int rssi, int ssid)
        {
            _dbContext.WIFI_DATA.Add(
                new WIFI_DATA
                {
                    AUTH_TYPE = authType,
                    BSSID = bssid,
                    WIFI_DATE = date,
                    DEVICE = device,
                    SSID = ssid,
                    RSSI = rssi,
                    WIFI_LOCATION = location
                });

            _dbContext.SaveChanges();
        }

        public USERS Authenticate(string login, string password)
        {
            var user = _dbContext.USERS.FirstOrDefault(u => u.LOGIN == login);
            if (user == null)
            {
                return null;
            }
            using (SHA512 sha512 = new SHA512Managed())
            {
                return user.PASSWORD == BitConverter.ToString(sha512.ComputeHash(Encoding.UTF8.GetBytes(password)))
                           .Replace("-", "")
                    ? user
                    : null;
            }
        }

        public string GetDevices()
        {
            var str = new StringBuilder();
            foreach (var el in _dbContext.DEVICES.Select(d => d.DEVICE_ID.TrimEnd()))
            {
                str.AppendLine(el.TrimEnd());
            }

            return str.ToString();
        }
    }
}