namespace AirportTicketBooking.Classes
{
    public class Passenger
    {
        public int PassportId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ValidationMessage { get; set; }

        public Passenger(int passportId, string name, string email, string phoneNumber)
        {
            PassportId = passportId;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public Passenger(string validationMassage)
        {
            ValidationMessage = validationMassage;
        }

        public override string ToString()
        {
            return $"{PassportId},{Name},{Email},{PhoneNumber}";
        }
    } 
}
