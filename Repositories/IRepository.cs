namespace AirportTicketBooking.Repositories
{
    public interface IRepository<T>
    {
        void Save(T data);

        List<T> Load();

        void DeleteTheFileData();

    }
}
