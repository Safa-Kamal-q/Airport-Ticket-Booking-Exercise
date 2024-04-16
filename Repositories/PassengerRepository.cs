using AirportTicketBooking.Classes;

namespace AirportTicketBooking.Repositories
{
    public class PassengerRepository: IRepository<Passenger>
    {
        private const string filePath = "passenger.csv";


        public void Save(Passenger passenger)
        {
            RepositoryHelper.SaveToFile(filePath, passenger);
        }

        public List<object> Load()
        {
            return RepositoryHelper.LoadFromFile(filePath, PassengerFromData);
        }

        public static Passenger PassengerFromData(string[] passengerData)
        {
            if (passengerData.Length != 4)
            {
                throw new FormatException("Invalid data format. Skipping line.");
            }

            int passportId = int.Parse(passengerData[0]);
            string name = passengerData[1];
            string email = passengerData[2];
            string phoneNumber = passengerData[3];

            var passenger = new Passenger(passportId, name, email, phoneNumber);

            return passenger;
        }
    }
}