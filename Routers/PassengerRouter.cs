using AirportTicketBooking.Classes;
using AirportTicketBooking.Repositories;

namespace AirportTicketBooking.Routers
{
    internal class PassengerRouter
    {
        private PassengerRepository _passengerRepository;

        public PassengerRouter()
        {
            _passengerRepository = new PassengerRepository();
        }

        public List<Passenger> GetAllData()
        {
            return _passengerRepository.Load();
        }

        public List<Passenger> GetAll()
        {
            return GetAllData().Where(passenger => passenger.ValidationMessage == null).ToList();
        }

        public bool TryGetByPassportId(int passportId, out Passenger passenger)
        {
            passenger = GetAll().FirstOrDefault(passenger => passenger.PassportId == passportId);
            return (passenger != null);
        }

        public void Add(Passenger passenger)
        {
            _passengerRepository.Save(passenger);
        }

        public void RemoveByPassportId(int passportId)
        {
            if (!TryGetByPassportId(passportId, out _))
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
