using AirportTicketBooking.Classes;

namespace AirportTicketBooking.Repositories
{
    public class BookingRepositoty: IRepository<Booking>
    {
        private const string FilePath = "booking.csv";

        public BookingRepositoty() { }
        
        public void Save(Booking booking)
        {
            RepositoryHelper.SaveToFile(FilePath, booking);
        }

        public List<Booking> Load()
        {
            return RepositoryHelper.LoadFromFile(FilePath, BookingFromData);
        }

        private Booking BookingFromData(string[] bookingData)
        {
            if (bookingData.Length != 5)
            {
                return new Booking("Invalid data format. Skipping line.");
            }

            int bookingId = int.Parse(bookingData[0]);
            int flightId = int.Parse(bookingData[1]);
            int passengerPassportId = int.Parse(bookingData[2]);
            DateTime bookingDate = DateTime.Parse(bookingData[3]);
            decimal price = decimal.Parse(bookingData[4]);

            var booking = new Booking(bookingId, flightId, passengerPassportId, bookingDate, price);

            return booking;
        }

        public void DeleteTheFileData()
        {
            RepositoryHelper.DeleteAllEntireData(FilePath);
        }
    }
}