namespace HotelRoomBookingSystem
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public string RoomType { get; set; } = string.Empty; // Single / Double / Suite
        public double PricePerNight { get; set; }
        public bool IsAvailable { get; set; } = true;

        public Room(int roomNumber, string roomType, double pricePerNight, bool isAvailable = true)
        {
            RoomNumber = roomNumber;
            RoomType = roomType;
            PricePerNight = pricePerNight;
            IsAvailable = isAvailable;
        }

        public override string ToString()
        {
            return $"Room {RoomNumber} ({RoomType}) - {PricePerNight:C} per night - " +
                   (IsAvailable ? "Available" : "Booked");
        }
    }
}
