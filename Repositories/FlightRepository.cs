using AirportTicketBooking.Classes;

namespace AirportTicketBooking.Controllers
{
    internal class FlightRepository
    {
        private const string filePath = "flights.csv";


        internal static void SaveToFile(Flight flight)
        {
            var writer = new StreamWriter(filePath, true);

            if (!File.Exists(filePath))
            {
                writer.WriteLine("FlightNumber,DepartureCountry,DestinationCountry," +
                                   "DepartureAirport,ArrivalAirport,LeaveDateTime,Price");
            }

            string flightData = $"{flight.FlightNumber},{flight.DepartureCountry},{flight.DestinationCountry}," +
                          $"{flight.DepartureAirport},{flight.ArrivalAirport},{flight.LeaveDateTime},{flight.Price}";

            writer.WriteLine(flightData);
        }

        internal static List<object> LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"{filePath} does not exist.");
            }

            //make it object not booking to add string when the reading line is Invaild format
            var fileData = new List<object>();

            var reader = new StreamReader(filePath);
            string line;
            reader.ReadLine(); //to skip the header line

            while ((line = reader.ReadLine()) != null)
            {
                string[] flightData = line.Split(',');
                if (flightData.Length != 7)
                {
                    fileData.Add("Invalid data format. Skipping line.");
                    continue;
                }

                int flightNumber = int.Parse(flightData[0]);
                string departureCountry = flightData[1];
                string destinationCountry = flightData[2];
                string departureAirport = flightData[3];
                string arrivalAirport = flightData[4];
                DateTime leaveDateTime = DateTime.Parse(flightData[5]);
                decimal price = decimal.Parse(flightData[6]);

                var flight = new Flight(flightNumber, departureCountry, destinationCountry, departureAirport, arrivalAirport, leaveDateTime, price, false);

                fileData.Add(flight);
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
