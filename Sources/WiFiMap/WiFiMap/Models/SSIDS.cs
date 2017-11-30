using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WiFiMap.Models
{
    public class SSIDS
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SSIDS()
        {
            WIFI_DATA = new HashSet<WIFI_DATA>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string SSID { get; set; }

        public bool BLOCKED { get; set; }

        public int BSSID { get; set; }

        public virtual BSSIDS BSSIDS { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WIFI_DATA> WIFI_DATA { get; set; }
    }
}
