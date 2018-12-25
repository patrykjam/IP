namespace WiFiServer.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class USERS
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
