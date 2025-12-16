using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.Text;

namespace ISR.Application.Tests.Leads.Upload
{
    internal static class TestFormFileFactory
    {
        public static IFormFile CreateCsv(string content, string fileName = "test.csv")
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            var stream = new MemoryStream(bytes);

            return new FormFile(stream, 0, bytes.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/csv"
            };
        }

        public static IFormFile CreateValidCsv()
        {
            var csv = """
            Name,Address,Latitude,Longitude
            Juan Perez,San Salvador,13.6929,-89.2182
            Maria Lopez,Soyapango,13.7102,-89.1390
            """;

            return CreateCsv(csv, "valid.csv");
        }

        public static IFormFile CreateEmptyCsv()
        {
            var csv = "Name,Address,Latitude,Longitude";
            return CreateCsv(csv, "empty.csv");
        }
    }
}
