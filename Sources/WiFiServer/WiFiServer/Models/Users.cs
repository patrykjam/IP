namespace WiFiServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
