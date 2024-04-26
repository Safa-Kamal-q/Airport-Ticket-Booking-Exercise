using AirportTicketBooking.Classes;
using AirportTicketBooking.Repositories;
using ClassType = TicketClassType.ClassType;

namespace AirportTicketBooking.Routers
{
    internal class BookingRouter
    {
        private BookingRepositoty _bookingRepositoty;
        private FlightRouter _flightRouter;

        public BookingRouter()
        {
            _bookingRepositoty = new BookingRepositoty();
        }

        public List<Booking> GetAllData()
        {
            return _bookingRepositoty.Load();
        }

        public List<Booking> GetAll()
        {
            return GetAllData().Where(booking => booking.ValidationMessage == null).ToList();
        }

        public bool TryGetByBookingId(int bookingId, out Booking booking)
        {
            booking = GetAll().FirstOrDefault(booking => booking.BookingId == bookingId);
            return (booking != null);
        }

        public void Add(Booking booking)
        {
            _bookingRepositoty.Save(booking);
        }

        public void RemoveByBookingId(int bookingId)
        {
            if (!TryGetByBookingId(bookingId, out _))
            {
                throw new Exception($"Booking with BookingId: {bookingId} doesn't exist");
            }

            var remainingBookings = GetAll().Where(booking => booking.BookingId != bookingId).ToList();

            _bookingRepositoty.DeleteTheFileData();

            foreach (var booking in remainingBookings)
            {
                _bookingRepositoty.Save(booking);
            }
        }

        public List<Booking> GetBookingForPassenger(int passengerPassportId)
        {
            return GetAll().Where(booking => booking.PassengerPassportId == passengerPassportId).ToList();
        }


        public List<Booking> FilterByPrice(decimal price)
        {
            return GetAll().Where(booking => booking.Price == price).ToList();
        }

        public List<Booking> FilterByDepartureCountry(string departureCountry)
        {
            return GetAll().Where(booking =>
                         _flightRouter.GetByFlightNumber(booking.FlightId).DepartureCountry
                         .Equals(departureCountry, StringComparison.InvariantCultureIgnoreCase))
                         .ToList();
        }

        public List<Booking> FilterByDestinationCountry(string destinationCountry)
        {
            return GetAll().Where(booking =>
                            _flightRouter.GetByFlightNumber(booking.FlightId).DestinationCountry
                            .Equals(destinationCountry, StringComparison.InvariantCultureIgnoreCase))
                            .ToList();
        }

        public List<Booking> FilterByDepartureDate(DateTime departureDateTime)
        {
            return GetAll().Where(booking =>
                            _flightRouter.GetByFlightNumber(booking.FlightId).DepartureDateTime == departureDateTime)
                            .ToList();
        }

        public List<Booking> FilterByDepartureAirport(string departureAirport)
        {
            return GetAll().Where(booking =>
                            _flightRouter.GetByFlightNumber(booking.FlightId).DepartureAirport
                            .Equals(departureAirport, StringComparison.InvariantCultureIgnoreCase))
                            .ToList();
        }

        public List<Booking> FilterByArrivalAirport(string arrivalAirport)
        {
            return GetAll().Where(booking =>
                            _flightRouter.GetByFlightNumber(booking.FlightId).ArrivalAirport
                            .Equals(arrivalAirport, StringComparison.InvariantCultureIgnoreCase))
                            .ToList();
        }

        public List<Booking> FilterByFlightClass(ClassType flightClass)
        {
            return GetAll().Where(booking =>
                            _flightRouter.GetByFlightNumber(booking.FlightId).ClassType == flightClass)
                            .ToList();
        }

        public List<Booking> FilterByFlightId(int flightId)
        {
            return GetAll().Where(booking => booking.FlightId == flightId).ToList();
        }

    }
}
