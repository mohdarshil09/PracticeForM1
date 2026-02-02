using System.Collections.Generic;
using System.Linq;

namespace HotelRoomBookingSystem
{
    public class HotelManager
    {
        private readonly List<Room> _rooms = new();

        // Adds room if room number doesn't exist
        public void AddRoom(int roomNumber, string type, double price)
        {
            if (_rooms.Any(r => r.RoomNumber == roomNumber))
            {
                return; // Room already exists, ignore
            }

            _rooms.Add(new Room(roomNumber, type, price));
        }

        // Groups available rooms by type
        public Dictionary<string, List<Room>> GroupRoomsByType()
        {
            var result = new Dictionary<string, List<Room>>();

            foreach (var room in _rooms.Where(r => r.IsAvailable))
            {
                if (!result.ContainsKey(room.RoomType))
                {
                    result[room.RoomType] = new List<Room>();
                }
                result[room.RoomType].Add(room);
            }

            return result;
        }

        // Books room if available, calculates total cost
        public bool BookRoom(int roomNumber, int nights)
        {
            var room = _rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room == null || !room.IsAvailable)
            {
                return false;
            }

            double totalCost = room.PricePerNight * nights;
            room.IsAvailable = false;

            Console.WriteLine($"Room {room.RoomNumber} booked for {nights} night(s). Total cost: {totalCost:C}");
            return true;
        }

        // Returns available rooms within price range
        public List<Room> GetAvailableRoomsByPriceRange(double min, double max)
        {
            return _rooms
                .Where(r => r.IsAvailable && r.PricePerNight >= min && r.PricePerNight <= max)
                .ToList();
        }
    }
}
