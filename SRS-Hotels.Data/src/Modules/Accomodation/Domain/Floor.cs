namespace SRS_Hotels.Data.src.Modules.Accomodation.Domain
{
    public class Floor
    {
        public Guid Id { get; set; }

        public int Number { get; set; }
        public string Name { get; set; } = null!;


        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
