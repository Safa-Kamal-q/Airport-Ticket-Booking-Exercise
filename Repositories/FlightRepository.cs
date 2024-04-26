using AirportTicketBooking.Classes;
using ClassType = TicketClassType.ClassType;

namespace AirportTicketBooking.Repositories
{
    public class FlightRepository: IRepository<Flight>
    {
        private const string FilePath = "flights.csv";

        public FlightRepository() { }

        public void Save(Flight flight)
        {
            RepositoryHelper.SaveToFile(FilePath, flight);
        }

        public List<Flight> Load()
        {
           return RepositoryHelper.LoadFromFile(FilePath, FlightFromData);
        }

        private Flight FlightFromData(string[] flightData)
        {
            if (flightData.Length != 9)
            {
                return new Flight("Invalid data format. Skipping line.");
            }

            int flightNumber = int.Parse(flightData[0]);
            string departureCountry = flightData[1];
            string destinationCountry = flightData[2];
            string departureAirport = flightData[3];
            string arrivalAirport = flightData[4];
            DateTime departureDateTime = DateTime.Parse(flightData[5]);
            int flightCapacity = int.Parse(flightData[6]);
            string classTypeString = flightData[7];

            bool isEnumParseSuccess = Enum.TryParse(classTypeString, out ClassType classType);

            if (!isEnumParseSuccess)
            {
                return new Flight("Incorrect stored data: Invalid class type");
            }

            var flight = new Flight(flightNumber, departureCountry, destinationCountry, departureAirport,
                                    arrivalAirport, departureDateTime, flightCapacity, classType);

            return flight;
        }

        public void DeleteTheFileData()
        {
            RepositoryHelper.DeleteAllEntireData(FilePath);
        }
    }
}
