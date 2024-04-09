namespace AirportTicketBooking.Classes
{
    internal class Booking
    {
        internal int BookingId { get; set; }
        internal Flight Flight { get; set; }

        internal Passenger Passenger { get; set; }

        internal DateTime BookingDate { get; set; }

        internal TicketClassType.ClassType ClassType { get; set; }

        internal Booking(int bookingId, Flight flight, Passenger passenger, 
                        DateTime bookingDate, TicketClassType.ClassType classType)
        {
            BookingId = bookingId;
            Flight = flight;
            Passenger = passenger;
            BookingDate = bookingDate;
            ClassType = classType;
        }
    }
}
