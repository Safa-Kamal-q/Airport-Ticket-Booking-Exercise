using AirportTicketBooking.Controllers;

namespace AirportTicketBooking.Classes
{
    internal class Flight
    {
        internal int FlightNumber { get; set; }
        internal string DepartureCountry { get; set; }
        internal string DestinationCountry { get; set; }
        internal string DepartureAirport { get; set; }
        internal string ArrivalAirport { get; set; }
        internal DateTime LeaveDateTime { get; set; }
        internal decimal Price { get; set; }

        internal int FlightCapacity { get; set; }

        internal Flight(int flightNumber, string departureCountry, string destinationCountry,
                        string departureAirport, string arrivalAirport, DateTime leaveDateTime, decimal price,
                        bool isNewFlight = true)
        {
            FlightNumber = flightNumber;        
            DepartureCountry = departureCountry;
            DestinationCountry = destinationCountry;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            LeaveDateTime = leaveDateTime;
            Price = price;

            if (isNewFlight)
            {
                FlightRepository.SaveToFile(this);
            }
        }
    }
}
