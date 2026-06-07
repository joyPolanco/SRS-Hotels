namespace SRS_Hotels.Data.src.Modules.Accomodation.Domain
{
    public class RoomAmenity
    {
        public Guid RoomId { get; set; }
        public Guid AmenityId { get; set; }

        public Room Room { get; set; } = null!;
        public Amenity Amenity { get; set; } = null!;
    }
}
