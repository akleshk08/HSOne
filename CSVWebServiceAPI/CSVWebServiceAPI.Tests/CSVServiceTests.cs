using CSVWebServiceAPI.Services;

namespace CSVWebServiceAPI.Tests
{
    [TestClass]
    public class CSVServiceTests
    {
        [TestMethod]
        public void FormatCSV_EmptyString_ReturnsEmptyString()
        {
            // Arrange
            string csvData = "";

            ICSVService csvService = new CSVService();

            // Act
            string result = csvService.FormatCSV(csvData);

            // Assert
            Assert.AreEqual("[]", result);
        }

        [TestMethod]
        public void FormatCSV_SingleField_ReturnsFormattedString()
        {
            // Arrange
            string csvData = "Patient Name";

            ICSVService csvService = new CSVService();

            // Act
            string result = csvService.FormatCSV(csvData);

            // Assert
            Assert.AreEqual("[Patient Name]", result);
        }

        [TestMethod]
        public void FormatCSV_MultipleFields_ReturnsFormattedString()
        {
            // Arrange
            string csvData = "Patient Name, SSN, Age";

            ICSVService csvService = new CSVService();

            // Act
            string result = csvService.FormatCSV(csvData);

            // Assert
            Assert.AreEqual("[Patient Name] [SSN] [Age]", result);
        }

        [TestMethod]
        public void FormatCSV_QuotedFields_ReturnsFormattedString()
        {
            // Arrange
            string csvData = "\"Patient Name\",\"SSN\",\"Age\"";

            ICSVService csvService = new CSVService();

            // Act
            string result = csvService.FormatCSV(csvData);

            // Assert
            Assert.AreEqual("[Patient Name] [SSN] [Age]", result);
        }

        [TestMethod]
        public void FormatCSV_QuotedFieldsWithCommas_ReturnsFormattedString()
        {
            // Arrange
            string csvData = "\"FirstName, LastName\",112233445,25";

            ICSVService csvService = new CSVService();

            // Act
            string result = csvService.FormatCSV(csvData);

            // Assert
            Assert.AreEqual("[FirstName, LastName] [112233445] [25]", result);
        }

        [TestMethod]
        public void FormatCSV_LeadingWhitespace_ReturnsFormattedStringWithoutWhitespace()
        {
            // Arrange
            string csvData = "  John , Michael ";

            ICSVService csvService = new CSVService();

            // Act
            string result = csvService.FormatCSV(csvData);

            // Assert
            Assert.AreEqual("[John] [Michael]", result);
        }

        [TestMethod]
        public void FormatCSV_GivenExample_ReturnsFormattedString()
        {
            // Arrange
            string inputCSV = "\"Patient Name\",\"SSN\",\"Age\",\"Phone Number\",\"Status\"\n" +
                              "\"Prescott, Zeke\",\"542-51-6641\",21,\"801-555-2134\",\"Opratory=2,PCP=1\"\n" +
                              "\"Goldstein, Bucky\",\"635-45-1254\",42,\"435-555-1541\",\"Opratory=1,PCP=1\"\n" +
                              "\"Vox, Bono\",\"414-45-1475\",51,\"801-555-2100\",\"Opratory=3,PCP=2\"";

            ICSVService csvService = new CSVService();

            // Act
            string formattedCSV = csvService.FormatCSV(inputCSV);

            // Assert
            string expectedFormattedCSV = "[Patient Name] [SSN] [Age] [Phone Number] [Status]\n" +
                                          "[Prescott, Zeke] [542-51-6641] [21] [801-555-2134] [Opratory=2,PCP=1]\n" +
                                          "[Goldstein, Bucky] [635-45-1254] [42] [435-555-1541] [Opratory=1,PCP=1]\n" +
                                          "[Vox, Bono] [414-45-1475] [51] [801-555-2100] [Opratory=3,PCP=2]";


            Assert.AreEqual(expectedFormattedCSV, formattedCSV);
        }
    }
}