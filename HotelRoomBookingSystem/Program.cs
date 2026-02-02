namespace HotelRoomBookingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var manager = new HotelManager();

            // 1. Add different room types with prices
            manager.AddRoom(101, "Single", 80.0);
            manager.AddRoom(102, "Single", 85.0);
            manager.AddRoom(201, "Double", 120.0);
            manager.AddRoom(202, "Double", 130.0);
            manager.AddRoom(301, "Suite", 250.0);

            // 2. Display available rooms grouped by type
            Console.WriteLine("=== Available Rooms Grouped by Type ===\n");
            var grouped = manager.GroupRoomsByType();
            foreach (var kvp in grouped)
            {
                Console.WriteLine($"Type: {kvp.Key}");
                foreach (var room in kvp.Value)
                {
                    Console.WriteLine($"  {room}");
                }
                Console.WriteLine();
            }

            // 3. Book a room for specific nights
            Console.WriteLine("=== Booking Room 201 for 3 nights ===");
            bool booked = manager.BookRoom(201, 3);
            Console.WriteLine(booked ? "Booking successful.\n" : "Booking failed.\n");

            // Show available rooms again after booking
            Console.WriteLine("=== Available Rooms After Booking ===\n");
            grouped = manager.GroupRoomsByType();
            foreach (var kvp in grouped)
            {
                Console.WriteLine($"Type: {kvp.Key}");
                foreach (var room in kvp.Value)
                {
                    Console.WriteLine($"  {room}");
                }
                Console.WriteLine();
            }

            // 4. Find rooms within budget
            Console.WriteLine("=== Rooms within budget 80 - 150 ===\n");
            var inBudget = manager.GetAvailableRoomsByPriceRange(80, 150);
            foreach (var room in inBudget)
            {
                Console.WriteLine(room);
            }

            if (inBudget.Count == 0)
            {
                Console.WriteLine("No rooms found in this price range.");
            }
        }
    }
}
