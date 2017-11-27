namespace WiFiServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ALL_DATA
    {
        public DateTime? WIFI_DATE { get; set; }

        [Key]
        [Column(Order = 0)]
        public double LONGITUDE { get; set; }

        [Key]
        [Column(Order = 1)]
        public double LATITUDE { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string DEVICE_ID { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string SSID { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(17)]
        public string BSSID { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string AUTH_TYPE { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RSSI { get; set; }
    }
}
