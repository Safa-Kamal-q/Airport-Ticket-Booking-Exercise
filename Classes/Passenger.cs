namespace AirportTicketBooking.Classes
{
    internal class Passenger
    {
        internal int PassportId { get; set; }
        internal string Name { get; set; }
        internal string Email { get; set; }
        internal string PhoneNumber { get; set; }

        internal Passenger(int passportId, string name, string email, string phoneNumber)
        {
            PassportId = passportId;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    
    } 
}
