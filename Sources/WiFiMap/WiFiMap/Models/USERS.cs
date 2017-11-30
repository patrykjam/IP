using System.ComponentModel.DataAnnotations;

namespace WiFiMap.Models
{
    public class USERS
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string LOGIN { get; set; }

        [Required]
        [StringLength(512)]
        public string PASSWORD { get; set; }
    }
}
