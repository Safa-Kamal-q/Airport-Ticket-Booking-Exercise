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

        public static List<object> LoadFromFile<T>(string filePath, Func<string[], T> parseFunc)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"{filePath} does not exist.");
            }

            var fileData = new List<object>();

            var reader = new StreamReader(filePath);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                try
                {
                    var parsedData = parseFunc(line.Split(','));
                    fileData.Add(parsedData);
                }
                catch (FormatException ex)
                {
                    fileData.Add(ex.Message);
                }

            }
            return fileData;
        }
    }
}
