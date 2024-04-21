namespace AirportTicketBooking.Classes
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int FlightId { get; set; }

        public int PassengerPassportId { get; set; }

        public DateTime BookingDate { get; set; }

        public decimal Price { get; set; }

        public string ValidationMessage { get; set; }

        public Booking(int bookingId, int flightId, int passengerPassportId, DateTime bookingDate, decimal price)
        {
            BookingId = bookingId;
            FlightId = flightId;
            PassengerPassportId = passengerPassportId;
            BookingDate = bookingDate;
            Price = price;
        }

        public Booking(string invalidationMassage)
        {
            ValidationMessage = invalidationMassage;
        }

        public override string ToString()
        {
            return $"{BookingId},{FlightId},{PassengerPassportId},{BookingDate},{Price}";
        }
    }
}
