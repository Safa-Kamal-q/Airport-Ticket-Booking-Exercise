using AirportTicketBooking.Controllers;

namespace AirportTicketBooking.Classes
{
    internal class Booking
    {
        internal int BookingId { get; set; }
        internal int FlightId { get; set; }

        internal int PassengerPassportId { get; set; }

        internal DateTime BookingDate { get; set; }

        internal TicketClassType.ClassType ClassType { get; set; }

        internal Booking(int bookingId, int flightId, int passengerPassportId, 
                        DateTime bookingDate, TicketClassType.ClassType classType, bool isNewBooking= true)
        {
            BookingId = bookingId;
            FlightId = flightId;
            PassengerPassportId = passengerPassportId;
            BookingDate = bookingDate;
            ClassType = classType;

            if (isNewBooking)
            {
                BookingRepositoty.SaveToFile(this);
            }
        }
    }
}
