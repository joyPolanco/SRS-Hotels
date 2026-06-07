using SRS_Hotels.Data.src.Modules.Accomodation.Domain.common.Enums;

namespace SRS_Hotels.Data.src.Modules.Accomodation.Domain
{
    public class Room
    {
        public Guid Id { get; set; }

        public string Number { get; set; } = null!;
        public int Capacity { get; set; }

        public Guid FloorId { get; set; }
        public Guid RoomCategoryId { get; set; }

        public RoomStatus Status { get; set; }

        public ICollection<RoomImage> Images { get; set; } = new List<RoomImage>();
        public ICollection<RoomAmenity> Amenities { get; set; } = new List<RoomAmenity>();
        public ICollection<RoomService> Services { get; set; } = new List<RoomService>();
        public ICollection<SeasonalPrice> SeasonalPrices { get; set; } = new List<SeasonalPrice>();
    }
}
