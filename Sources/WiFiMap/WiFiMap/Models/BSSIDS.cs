using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WiFiMap.Models
{
    public class BSSIDS
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WIFI_DATA> WIFI_DATA { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SSIDS> SSIDS { get; set; }
    }
}
