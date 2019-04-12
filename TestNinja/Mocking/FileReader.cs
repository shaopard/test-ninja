using System.IO;

namespace TestNinja.Mocking
{
    public class FileReader : IFileReader
    {
        public string Read(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
