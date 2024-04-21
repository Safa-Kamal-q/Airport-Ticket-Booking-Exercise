using ClassType = TicketClassType.ClassType;

namespace AirportTicketBooking.Classes
{
    public class Flight
    {
        public int FlightNumber { get; set; }

        public string DepartureCountry { get; set; }

        public string DestinationCountry { get; set; }

        public string DepartureAirport { get; set; }

        public string ArrivalAirport { get; set; }

        public DateTime DepartureDateTime { get; set; }

        public int FlightCapacity { get; set; }

        public ClassType ClassType { get; set; }

        public string ValidationMessage { get; set; }

        public Flight(int flightNumber, string departureCountry, string destinationCountry,
                        string departureAirport, string arrivalAirport, DateTime departureDateTime,
                        int flightCapacity, ClassType classType)
        {
            FlightNumber = flightNumber;        
            DepartureCountry = departureCountry;
            DestinationCountry = destinationCountry;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            DepartureDateTime = departureDateTime;
            FlightCapacity= flightCapacity;
            ClassType = classType;
        }

        public Flight(string invalidationMassage)
        {
            ValidationMessage = invalidationMassage;
        }

        public override string ToString()
        {
            return $"{FlightNumber},{DepartureCountry},{DestinationCountry},{DepartureAirport}," +
                    $"{ArrivalAirport},{DepartureDateTime},{FlightCapacity},{ClassType}";
        }
    }
}
