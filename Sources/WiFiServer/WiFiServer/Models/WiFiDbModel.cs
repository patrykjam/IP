using System.Data.Entity;

namespace WiFiServer.Models
{
    public partial class WiFiDbModel : DbContext
    {
        public WiFiDbModel()
            : base("name=WiFiDbModel")
        {
        }

        public virtual DbSet<AUTH_TYPES> AUTH_TYPES { get; set; }
        public virtual DbSet<BSSIDS> BSSIDS { get; set; }
        public virtual DbSet<DATES> DATES { get; set; }
        public virtual DbSet<DEVICES> DEVICES { get; set; }
        public virtual DbSet<LOCATIONS> LOCATIONS { get; set; }
        public virtual DbSet<SSIDS> SSIDS { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }
        public virtual DbSet<WIFI_DATA> WIFI_DATA { get; set; }
        public virtual DbSet<ALL_DATA> ALL_DATA { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AUTH_TYPES>()
                .Property(e => e.AUTH_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<AUTH_TYPES>()
                .HasMany(e => e.WIFI_DATA)
                .WithRequired(e => e.AUTH_TYPES)
                .HasForeignKey(e => e.AUTH_TYPE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BSSIDS>()
                .Property(e => e.BSSID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BSSIDS>()
                .HasMany(e => e.WIFI_DATA)
                .WithRequired(e => e.BSSIDS)
                .HasForeignKey(e => e.BSSID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BSSIDS>()
                .HasMany(e => e.SSIDS)
                .WithRequired(e => e.BSSIDS)
                .HasForeignKey(e => e.BSSID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DATES>()
                .HasMany(e => e.WIFI_DATA)
                .WithRequired(e => e.DATES)
                .HasForeignKey(e => e.WIFI_DATE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DEVICES>()
                .Property(e => e.DEVICE_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DEVICES>()
                .HasMany(e => e.WIFI_DATA)
                .WithRequired(e => e.DEVICES)
                .HasForeignKey(e => e.DEVICE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LOCATIONS>()
                .HasMany(e => e.WIFI_DATA)
                .WithRequired(e => e.LOCATIONS)
                .HasForeignKey(e => e.WIFI_LOCATION)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SSIDS>()
                .Property(e => e.SSID)
                .IsUnicode(false);

            modelBuilder.Entity<SSIDS>()
                .HasMany(e => e.WIFI_DATA)
                .WithRequired(e => e.SSIDS)
                .HasForeignKey(e => e.SSID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ALL_DATA>()
                .Property(e => e.DEVICE_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALL_DATA>()
                .Property(e => e.SSID)
                .IsUnicode(false);

            modelBuilder.Entity<ALL_DATA>()
                .Property(e => e.BSSID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ALL_DATA>()
                .Property(e => e.AUTH_TYPE)
                .IsUnicode(false);
        }
    }
}
