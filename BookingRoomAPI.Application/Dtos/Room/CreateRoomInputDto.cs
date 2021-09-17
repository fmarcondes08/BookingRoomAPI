
namespace BookingRoomAPI.Application.Dtos.Room
{
    public class CreateRoomInputDto
    {
        public int Number { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }
    }
}
