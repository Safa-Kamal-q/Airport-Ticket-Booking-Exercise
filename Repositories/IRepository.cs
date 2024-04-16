namespace AirportTicketBooking.Repositories
{
    public interface IRepository<T>
    {
        void Save(T data);

        List<object> Load();

    }
}
