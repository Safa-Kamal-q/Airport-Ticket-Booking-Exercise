namespace AirportTicketBooking.Classes
{
    internal class Flight
    {
        internal string FlightNumber { get; set; }
        internal string DepartureCountry { get; set; }
        internal string DestinationCountry { get; set; }
        internal string DepartureAirport { get; set; }
        internal string ArrivalAirport { get; set; }
        internal DateTime LeaveDateTime { get; set; }
        internal decimal Price { get; set; }

        internal int FlightCapacity { get; set; }

        internal Flight(string flightNumber, string departureCountry, string destinationCountry,
                        string departureAirport, string arrivalAirport, DateTime leaveDateTime, decimal price)
        {
            FlightNumber = flightNumber;        
            DepartureCountry = departureCountry;
            DestinationCountry = destinationCountry;
            ArrivalAirport = arrivalAirport;
            LeaveDateTime = leaveDateTime;
            Price = price;

        }
    }
}
