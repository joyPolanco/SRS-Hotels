using Microsoft.EntityFrameworkCore;
using SRS_Hotels.Data.src.Infraestructure.Identity.Entities;
using SRS_Hotels.Data.src.Modules.Accomodation.Domain;
using SRS_Hotels.Data.src.Modules.Audit.Domain;
using SRS_Hotels.Data.src.Modules.Authentication.Domain;
using SRS_Hotels.Data.src.Modules.Bookings.Domain;
using SRS_Hotels.Data.src.Modules.Customers.Domain;
using SRS_Hotels.Data.src.Modules.Employees.Domain;


namespace SRS_Hotels.Data.src.Infraestructure.Persistence.Contexts
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options)
        {
        }

        // 🔷 Authentication
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        // 🔷 Customers
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<LoyaltyAccount> LoyaltyAccounts { get; set; }

        // 🔷 Employees
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }

        // 🔷 Accommodation
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<RoomService> RoomServices { get; set; }
        public DbSet<SeasonalPrice> SeasonalPrices { get; set; }

        // 🔷 Bookings
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingGuest> BookingGuests { get; set; }
        public DbSet<BookingHistory> BookingHistories { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }

        // 🔷 Promotions
        public DbSet<Promotion> Promotions { get; set; }
   

        // 🔷 Cancellation Policies
        public DbSet<CancellationPolicy> CancellationPolicies { get; set; }
        public DbSet<CancellationRule> CancellationRules { get; set; }

    

        // 🔷 Audit
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HotelContext).Assembly);
        }
    }
}