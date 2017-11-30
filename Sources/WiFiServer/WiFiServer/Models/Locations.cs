namespace WiFiServer.Models
{
    using System.Collections.Generic;

    public partial class LOCATIONS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOCATIONS()
        {
            WIFI_DATA = new HashSet<WIFI_DATA>();
        }

        public int ID { get; set; }

        public double LONGITUDE { get; set; }

        public double LATITUDE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WIFI_DATA> WIFI_DATA { get; set; }
    }
}
