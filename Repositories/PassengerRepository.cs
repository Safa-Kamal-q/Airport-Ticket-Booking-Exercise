using AirportTicketBooking.Classes;

namespace AirportTicketBooking.Controllers
{
    internal class PassengerRepository
    {
        private const string filePath = "passenger.csv";


        internal static void SaveToFile(Passenger passenger)
        {
            var writer = new StreamWriter(filePath, true);

            if (!File.Exists(filePath))
            {
                writer.WriteLine("PassportId,Name,Email,PhoneNumber");

            }

            string passengerDate = $"{passenger.PassportId},{passenger.Name},{passenger.Email}, {passenger.PhoneNumber}";

            writer.WriteLine(passengerDate);
        }

        internal static List<object> LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"{filePath} does not exist.");
            }

            //make it object not Passenger to add string when the reading line is Invaild format
            var fileData = new List<object>();

            var reader = new StreamReader(filePath);
            string line;
            reader.ReadLine(); //to skip the header line

            while ((line = reader.ReadLine()) != null)
            {
                string[] passengerData = line.Split(',');
                if (passengerData.Length != 4)
                {
                    fileData.Add("Invalid data format. Skipping line.");
                    continue;
                }

                int passportId = int.Parse(passengerData[0]);
                string name = passengerData[1];
                string email = passengerData[2];
                string phoneNumber = passengerData[3];

                var passenger = new Passenger(passportId, name, email, phoneNumber, false);

                fileData.Add(passenger);
            }
            return fileData;
        }
        public static void Delete()
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}

