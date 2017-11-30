using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WiFiMap.Models
{
    public class LOCATIONS
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOCATIONS()
        {
            WIFI_DATA = new HashSet<WIFI_DATA>();
        }

        public int ID { get; set; }

        public double LONGITUDE { get; set; }

        public double LATITUDE { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WIFI_DATA> WIFI_DATA { get; set; }
    }
}
