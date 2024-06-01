using AirportTicketBooking.Classes;
using AirportTicketBooking.Repositories;

namespace AirportTicketBooking.Routers
{
    internal class PassengerService
    {
        private PassengerRepository _passengerRepository;

        public PassengerService()
        {
            _passengerRepository = new PassengerRepository();
        }

        private List<Passenger> GetAllData()
        {
            return _passengerRepository.Load();
        }

        public List<Passenger> GetAll()
        {
            return GetAllData().Where(passenger => passenger.ValidationMessage == null).ToList();
        }

        public bool TryGet(int passportId, out Passenger passenger)
        {
            passenger = GetAll().FirstOrDefault(passenger => passenger.PassportId == passportId);
            return (passenger != null);
        }

        public void Add(Passenger passenger)
        {
            _passengerRepository.Save(passenger);
        }

        public void RemoveBy(int passportId)
        {
            if (!TryGet(passportId, out _))
            {
                throw new Exception($"Passenger with PassportId: {passportId} doesn't exist");
            }

            var remainingPassengers = GetAll().Where(passenger => passenger.PassportId != passportId).ToList();

            _passengerRepository.DeleteTheFileData();

            foreach (var booking in remainingPassengers)
            {
                _passengerRepository.Save(booking);
            }
        }

    }
}
