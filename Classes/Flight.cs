using ClassType = TicketClassType.ClassType;

namespace AirportTicketBooking.Classes
{
    public class Flight
    {
        internal int FlightNumber { get; set; }

        internal string DepartureCountry { get; set; }

        internal string DestinationCountry { get; set; }

        internal string DepartureAirport { get; set; }

        internal string ArrivalAirport { get; set; }

        internal DateTime DepartureDate { get; set; }

        internal decimal Price { get; set; }

        internal int FlightCapacity { get; set; }

        internal ClassType ClassType { get; set; }

        internal Flight(int flightNumber, string departureCountry, string destinationCountry,
                        string departureAirport, string arrivalAirport, DateTime departureDate, decimal price,
                        int flightCapacity, ClassType classType)
        {
            FlightNumber = flightNumber;        
            DepartureCountry = departureCountry;
            DestinationCountry = destinationCountry;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            DepartureDate = departureDate;
            Price = price;
            FlightCapacity= flightCapacity;
            ClassType = classType;
        }

        public override string ToString()
        {
            return $"{FlightNumber},{DepartureCountry},{DestinationCountry},{DepartureAirport}," +
                    $"{ArrivalAirport},{DepartureDate},{Price},{FlightCapacity},{ClassType}";
        }
    }
}
