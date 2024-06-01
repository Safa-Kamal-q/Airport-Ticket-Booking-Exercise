using AirportTicketBooking.Classes;
using AirportTicketBooking.Repositories;
using ClassType = TicketClassType.ClassType;

namespace AirportTicketBooking.Routers
{
    internal class BookingService
    {
        private BookingRepository _bookingRepositoty;
        private FlightService _flightRouter;

        public BookingService()
        {
            _bookingRepositoty = new BookingRepository();
        }

        private List<Booking> GetAllData()
        {
            return _bookingRepositoty.Load();
        }

        public IEnumerable<Booking> GetAll()
        {
            return GetAllData().Where(booking => booking.ValidationMessage == null);
        }

        public bool TryGet(int bookingId, out Booking booking)
        {
            booking = GetAll().FirstOrDefault(booking => booking.BookingId == bookingId);
            return (booking != null);
        }

        public void Add(Booking booking)
        {
            _bookingRepositoty.Save(booking);
        }

        public void RemoveBy(int bookingId)
        {
            if (!TryGet(bookingId, out _))
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

        public List<Booking> FilterBy(ref List<string> errorList, string passengerPassportId = null, string price = null,
                                      string departureCountry = null, string destinationCountry = null,
                                      string departureDateTime = null, string departureAirport = null,
                                      string arrivalAirport = null, string classType = null, string flightId = null)
        {
            IEnumerable<Booking> bookings = GetAll();
            errorList = [];

            if (passengerPassportId != null)
            {
                if (int.TryParse(passengerPassportId, out int iPassengerPassportId)) bookings = GetBooking(bookings, iPassengerPassportId);
                else errorList.Add($"Invalid Input, {passengerPassportId} ");
            }

            if (price != null)
            {
                if (decimal.TryParse(price, out decimal dPrice)) bookings = FilterByPrice(bookings, dPrice);
                else errorList.Add($"Invalid Input, {price} ");
            }

            if (departureCountry != null) bookings = FilterByDepartureCountry(bookings, departureCountry);

            if (destinationCountry != null) bookings = FilterByDestinationCountry(bookings, destinationCountry);

            if (departureDateTime != null)
            {
                if (DateTime.TryParse(departureDateTime, out DateTime dtDepartureDateTime)) bookings = FilterByDepartureDate(bookings, dtDepartureDateTime);
                else errorList.Add($"Invalid Input, {departureDateTime} ");
            }
            if (departureAirport != null) bookings = FilterByDepartureAirport(bookings, departureAirport);

            if (arrivalAirport != null) bookings = FilterByArrivalAirport(bookings, arrivalAirport);

            if (classType != null)
            {
                if (Enum.TryParse(classType, out ClassType parsedClassType)) bookings = FilterByClassType(bookings, parsedClassType);
                else errorList.Add($"Invalid Input, {classType} ");
            }

            if (flightId != null)
            {
                if (int.TryParse(flightId, out int iFlightId)) bookings = FilterByFlightId(bookings, iFlightId);
                else errorList.Add($"Invalid Input, {flightId} ");
            }

            return bookings.ToList();
        }

        private IEnumerable<Booking> GetBooking(IEnumerable<Booking> bookings, int passengerPassportId)
        {
            return bookings.Where(booking => booking.PassengerPassportId == passengerPassportId);
        }


        private IEnumerable<Booking> FilterByPrice(IEnumerable<Booking> bookings, decimal price)
        {
            return bookings.Where(booking => booking.Price == price);
        }

        private IEnumerable<Booking> FilterByDepartureCountry(IEnumerable<Booking> bookings, string departureCountry)
        {
            return bookings.Where(booking =>
                         _flightRouter.GetBy(booking.FlightId).DepartureCountry
                         .Equals(departureCountry, StringComparison.InvariantCultureIgnoreCase));
        }

        private IEnumerable<Booking> FilterByDestinationCountry(IEnumerable<Booking> bookings, string destinationCountry)
        {
            return bookings.Where(booking =>
                            _flightRouter.GetBy(booking.FlightId).DestinationCountry
                            .Equals(destinationCountry, StringComparison.InvariantCultureIgnoreCase));
        }

        private IEnumerable<Booking> FilterByDepartureDate(IEnumerable<Booking> bookings, DateTime departureDateTime)
        {
            return bookings.Where(booking =>
                            _flightRouter.GetBy(booking.FlightId).DepartureDateTime == departureDateTime);
        }

        private IEnumerable<Booking> FilterByDepartureAirport(IEnumerable<Booking> bookings, string departureAirport)
        {
            return bookings.Where(booking =>
                            _flightRouter.GetBy(booking.FlightId).DepartureAirport
                            .Equals(departureAirport, StringComparison.InvariantCultureIgnoreCase));
        }

        private IEnumerable<Booking> FilterByArrivalAirport(IEnumerable<Booking> bookings, string arrivalAirport)
        {
            return bookings.Where(booking =>
                            _flightRouter.GetBy(booking.FlightId).ArrivalAirport
                            .Equals(arrivalAirport, StringComparison.InvariantCultureIgnoreCase));
        }

        private IEnumerable<Booking> FilterByClassType(IEnumerable<Booking> bookings, ClassType classType)
        {
            return bookings.Where(booking =>
                            _flightRouter.GetBy(booking.FlightId).ClassType == classType);
        }

        private IEnumerable<Booking> FilterByFlightId(IEnumerable<Booking> bookings, int flightId)
        {
            return bookings.Where(booking => booking.FlightId == flightId);
        }

    }
}
