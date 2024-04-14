using AirportTicketBooking.Classes;

namespace AirportTicketBooking.Controllers
{
    internal class BookingRepositoty
    {
        private const string filePath = "booking.csv";


        internal static void SaveToFile(Booking booking)
        {
            var writer = new StreamWriter(filePath, true);

            if (!File.Exists(filePath))
            {
                writer.WriteLine("BookingId,FlightId,PassengerPassportId," +
                                   "BookingDate,ClassType");
            }

            string flightData = $"{booking.BookingId}, {booking.FlightId}, {booking.PassengerPassportId}," +
                                 $"{booking.BookingDate}, {booking.ClassType}";

            writer.WriteLine(flightData);
        }

        internal static List<object> LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"{filePath} does not exist.");
            }

            //make it object not Flight to add string when the reading line is Invaild format
            var fileData = new List<object>();

            var reader = new StreamReader(filePath);
            string line;
            reader.ReadLine(); //to skip the header line

            while ((line = reader.ReadLine()) != null)
            {
                string[] bookingData = line.Split(',');

                if (bookingData.Length != 5)
                {
                    fileData.Add("Invalid data format. Skipping line.");
                    continue;
                }

                int bookingId = int.Parse(bookingData[0]);
                int flightId = int.Parse(bookingData[1]);
                int passengerPassportId = int.Parse(bookingData[2]);
                DateTime bookingDate = DateTime.Parse(bookingData[3]);
                string classTypeString = bookingData[4];

                //Surly this parse will success since I make a validation middleware before save int the file
                Enum.TryParse(classTypeString, out TicketClassType.ClassType classType);

                var booking = new Booking(bookingId, flightId, passengerPassportId, bookingDate, classType, false);

                fileData.Add(booking);
            }
            return fileData;
        }

        public static void Delete()
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}

