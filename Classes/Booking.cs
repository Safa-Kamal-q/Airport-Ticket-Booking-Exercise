namespace AirportTicketBooking.Classes
{
    public class Booking
    {
        internal int BookingId { get; set; }

        internal int FlightId { get; set; }

        internal int PassengerPassportId { get; set; }

        internal DateTime BookingDate { get; set; }

        internal decimal Price { get; set; }

        internal Booking(int bookingId, int flightId, int passengerPassportId, DateTime bookingDate, decimal price)
        {
            BookingId = bookingId;
            FlightId = flightId;
            PassengerPassportId = passengerPassportId;
            BookingDate = bookingDate;
            Price = price;
        }

        public override string ToString()
        {
            return $"{BookingId},{FlightId},{PassengerPassportId},{BookingDate},{Price}";
        }
    }
}
