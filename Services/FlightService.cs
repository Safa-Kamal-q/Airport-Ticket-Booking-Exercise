using AirportTicketBooking.Classes;
using AirportTicketBooking.Repositories;

namespace AirportTicketBooking.Routers
{
    public class FlightService
    {
        private FlightRepository _flightRepository;
        private BookingService _bookingRouter;

        public FlightService()
        {
            _flightRepository = new FlightRepository();
            _bookingRouter = new BookingService();
        }

        private List<Flight> GetAllData()
        {
            return _flightRepository.Load();
        }

        public IEnumerable<Flight> GetAll()
        {
            return GetAllData().Where(flight => flight.ValidationMessage == null);
        }

        public Flight GetBy(int flightNumber)
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

        public void RemoveBy(int flightNumber)
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

        private bool IsExist(int flightNumber)
        {
            var flight = GetAll().FirstOrDefault(flight => flight.FlightNumber == flightNumber);
            return flight != null;
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

        public List<Flight> FilterBy(ref List<string> errorList, string departureCountry = null,
                                      string destinationCountry = null, string departureDateTime = null,
                                      string departureAirport = null, string arrivalAirport = null)
        {
            IEnumerable<Flight> flights = GetAll();
            errorList = [];

            if (departureCountry != null) flights = FilterByDepartureCountry(flights, departureCountry);

            if (destinationCountry != null) flights = FilterByDestinationCountry(flights, destinationCountry);

            if (departureDateTime != null)
            {
                if (DateTime.TryParse(departureDateTime, out DateTime dtDepartureDateTime)) flights = FilterByDepartureDate(flights, dtDepartureDateTime);
                else errorList.Add($"Invalid Input, {departureDateTime} ");
            }

            if (departureAirport != null) flights = FilterByDepartureAirport(flights, departureAirport);

            if (arrivalAirport != null) flights = FilterByArrivalAirport(flights, arrivalAirport);

            return flights.ToList();
        }

        private IEnumerable<Flight> FilterByDepartureCountry(IEnumerable<Flight> flights, string departureCountry)
        {
            return flights.Where(flight => flight.DepartureCountry == departureCountry);
        }

        private IEnumerable<Flight> FilterByDestinationCountry(IEnumerable<Flight> flights, string destinationCountry)
        {
            return flights.Where(flight => flight.DestinationCountry == destinationCountry);
        }

        private IEnumerable<Flight> FilterByDepartureDate(IEnumerable<Flight> flights, DateTime departureDateTime)
        {
            return flights.Where(flight => flight.DepartureDateTime == departureDateTime);
        }

        private IEnumerable<Flight> FilterByDepartureAirport(IEnumerable<Flight> flights, string departureAirport)
        {
            return flights.Where(flight => flight.DepartureAirport == departureAirport);
        }

        private IEnumerable<Flight> FilterByArrivalAirport(IEnumerable<Flight> flights, string arrivalAirport)
        {
            return flights.Where(flight => flight.ArrivalAirport == arrivalAirport);
        }
    }
}
