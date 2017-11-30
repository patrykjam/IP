namespace WiFiServer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class BSSIDS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BSSIDS()
        {
            WIFI_DATA = new HashSet<WIFI_DATA>();
            SSIDS = new HashSet<SSIDS>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(17)]
        public string BSSID { get; set; }

        public bool BLOCKED { get; set; }

        public bool IS_5_GHz { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WIFI_DATA> WIFI_DATA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SSIDS> SSIDS { get; set; }
    }
}
