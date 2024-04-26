using AirportTicketBooking.Classes;

namespace AirportTicketBooking.Repositories
{
    public class PassengerRepository: IRepository<Passenger>
    {
        private const string FilePath = "passenger.csv";

        public PassengerRepository() { }

        public void Save(Passenger passenger)
        {
            RepositoryHelper.SaveToFile(FilePath, passenger);
        }

        public List<Passenger> Load()
        {
            return RepositoryHelper.LoadFromFile(FilePath, PassengerFromData);
        }

        private Passenger PassengerFromData(string[] passengerData)
        {
            if (passengerData.Length != 4)
            {
                return new Passenger("Invalid data format. Skipping line.");
            }

            int passportId = int.Parse(passengerData[0]);
            string name = passengerData[1];
            string email = passengerData[2];
            string phoneNumber = passengerData[3];

            var passenger = new Passenger(passportId, name, email, phoneNumber);

            return passenger;
        }

        public void DeleteTheFileData()
        {
            RepositoryHelper.DeleteAllEntireData(FilePath);
        }
    }
}