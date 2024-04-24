using System.Net;
using System.Text;

namespace CSVWebServiceIntegrationTests
{
    [TestClass]
    public class CSVServiceIntegrationTests
    {
        private const string BaseUrl = "https://localhost:7241";
        
        [TestMethod]
        public async Task GetFormattedCSV_Should_Return_Formatted_CSV_Data()
        {
            // Arrange
            using var client = new HttpClient();
            var requestUrl = $"{BaseUrl}/csv";

            // Create a sample request body if needed
            string inputCSV = "\"Patient Name\",\"SSN\",\"Age\",\"Phone Number\",\"Status\"\n" +
                  "\"Prescott, Zeke\",\"542-51-6641\",21,\"801-555-2134\",\"Opratory=2,PCP=1\"";                  
                 

            var requestBody = new StringContent(inputCSV, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(requestUrl, requestBody);
            response.EnsureSuccessStatusCode(); // Throws if HTTP response status code is not success

            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.IsNotNull(content);
            Assert.IsTrue(content.Contains("[Patient Name] [SSN] [Age] [Phone Number] [Status]"));
            Assert.IsTrue(content.Contains("[Prescott, Zeke] [542-51-6641] [21] [801-555-2134] [Opratory=2,PCP=1]"));            
        }
    }
}
