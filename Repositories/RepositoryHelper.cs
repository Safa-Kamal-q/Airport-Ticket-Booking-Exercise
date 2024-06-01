namespace AirportTicketBooking.Repositories
{
    public class RepositoryHelper
    {
        public static void SaveToFile<T>(string filePath, T data)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"{filePath} does not exist.");
            }

            var writer = new StreamWriter(filePath, true);
            writer.WriteLine(data.ToString());
        }

        public static List<T> LoadFromFile<T>(string filePath, Func<string[], T> parseFunc)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"{filePath} does not exist.");
            }

            var fileData = new List<T>();

            var reader = new StreamReader(filePath);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                var parsedData = parseFunc(line.Split(','));
                fileData.Add(parsedData);
            }
            return fileData;
        }

        public static void DeleteAllEntireData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"{filePath} does not exist.");
            }

            var writer = new StreamWriter(filePath, false);

            writer.BaseStream.SetLength(0);
        }
    }
}
