using AirportTicketBooking.Classes;
using ClassType = TicketClassType.ClassType;

namespace AirportTicketBooking.Repositories
{
    public class FlightRepository: IRepository<Flight>
    {
        private const string filePath = "flights.csv";


        public void Save(Flight flight)
        {
            RepositoryHelper.SaveToFile(filePath, flight);
        }

        public List<object> Load()
        {
           return RepositoryHelper.LoadFromFile(filePath, FlightFromData);
        }

        public static Flight FlightFromData(string[] flightData)
        {
            if (flightData.Length != 9)
            {
                throw new Exception("Invalid data format. Skipping line.");
            }

            int flightNumber = int.Parse(flightData[0]);
            string departureCountry = flightData[1];
            string destinationCountry = flightData[2];
            string departureAirport = flightData[3];
            string arrivalAirport = flightData[4];
            DateTime departureDate = DateTime.Parse(flightData[5]);
            decimal price = decimal.Parse(flightData[6]);
            int flightCapacity = int.Parse(flightData[7]);
            string classTypeString = flightData[4];

            bool isEnumParseSuccess = Enum.TryParse(classTypeString, out ClassType classType);

            if (!isEnumParseSuccess)
            {
                throw new FormatException("Incorrect stored data: Invalid class type");
            }

            var flight = new Flight(flightNumber, departureCountry, destinationCountry, departureAirport,
                                    arrivalAirport, departureDate, price, flightCapacity, classType);

            return flight;
        }
    }
}
