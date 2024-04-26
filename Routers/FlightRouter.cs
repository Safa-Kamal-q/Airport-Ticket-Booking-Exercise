using AirportTicketBooking.Classes;
using AirportTicketBooking.Repositories;

namespace AirportTicketBooking.Routers
{
    public class FlightRouter
    {
        private FlightRepository _flightRepository;
        private BookingRouter _bookingRouter;

        public FlightRouter()
        {
            _flightRepository = new FlightRepository();
            _bookingRouter = new BookingRouter();
        }

        public List<Flight> GetAllData()
        {
            return _flightRepository.Load();
        }

        public List<Flight> GetAll()
        {
            return GetAllData().Where(flight => flight.ValidationMessage == null).ToList();
        }

        public Flight GetByFlightNumber(int flightNumber)
        {
            var flight = GetAll().FirstOrDefault(flight => flight.FlightNumber == flightNumber);

            if (flight == null)
            {
                throw new Exception($"Flight with FlightNumber: {flightNumber} doesn't exist");

            }
            return flight;
        }

        public void Add(Flight flight)
        {
            _flightRepository.Save(flight);
        }

        public void RemoveByFlightNumber(int flightNumber)
        {
            if (IsExist(flightNumber))
            {
                throw new Exception($"Flight with FlightNumber: {flightNumber} doesn't exist");
            }

            var remainingFlights = GetAll().Where(flight => flight.FlightNumber != flightNumber).ToList();

            _flightRepository.DeleteTheFileData();

            foreach (var flight in remainingFlights)
            {
                _flightRepository.Save(flight);
            }
        }

        public List<Flight> GetAllAvailableForBooking()
        {
            return GetAll().Where(flight => IsAvailableForBooking(flight)).ToList();
        }

        public bool IsAvailableForBooking(Flight flight)
        {
            int numberOfPassengers = _bookingRouter.GetAll()
                .Where(booking => booking.FlightId == flight.FlightNumber)
                .Count();

            return flight.FlightCapacity > numberOfPassengers;
        }

        public List<Flight> FilterByDepartureCountry(string departureCountry)
        {
            return GetAll().Where(flight => flight.DepartureCountry == departureCountry).ToList();
        }

        public List<Flight> FilterByDestinationCountry(string destinationCountry)
        {
            return GetAll().Where(flight => flight.DestinationCountry == destinationCountry).ToList();
        }

        public List<Flight> FilterByDepartureDate(DateTime departureDateTime)
        {
            return GetAll().Where(flight => flight.DepartureDateTime == departureDateTime).ToList();
        }

        public List<Flight> FilterByDepartureAirport(string departureAirport)
        {
            return GetAll().Where(flight => flight.DepartureAirport == departureAirport).ToList();
        }

        public List<Flight> FilterByArrivalAirport(string arrivalAirport)
        {
            return GetAll().Where(flight => flight.ArrivalAirport == arrivalAirport).ToList();
        }

        public bool IsExist(int flightNumber)
        {
            var flight = GetAll().FirstOrDefault(flight => flight.FlightNumber == flightNumber);
            return flight != null;
        }

    }
}
