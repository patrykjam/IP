namespace WiFiServer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class DEVICES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DEVICES()
        {
            WIFI_DATA = new HashSet<WIFI_DATA>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(16)]
        public string DEVICE_ID { get; set; }

        public bool BLOCKED { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WIFI_DATA> WIFI_DATA { get; set; }
    }
}
