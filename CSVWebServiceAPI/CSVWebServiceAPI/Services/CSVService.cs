using System.Text;

namespace CSVWebServiceAPI.Services
{
    public class CSVService: ICSVService
    {
        public string FormatCSV(string csvData)
        {
            StringBuilder formattedCSV = new StringBuilder();

            bool inQuotes = false;
            StringBuilder fieldValue = new StringBuilder();

            foreach (char c in csvData)
            {
                if (c == '"')
                {
                    // Toggle inQuotes when encountering a double quote
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    // If not within quotes and encountering a comma, append the formatted field value
                    formattedCSV.Append($"[{fieldValue.ToString().Trim()}] ");
                    fieldValue.Clear(); // Clear the fieldValue for the next field
                }
                else if (c == '\n' || c == '\r')
                {
                    // If encountering a newline, append the formatted field value and a new line character
                    formattedCSV.Append($"[{fieldValue.ToString().Trim()}]");
                    fieldValue.Clear(); // Clear the fieldValue for the next line
                    formattedCSV.Append($"\n");  // Append a new line character
                }
                else
                {
                    // Append the character to the fieldValue
                    fieldValue.Append(c);
                }
            }

            // Append any remaining fieldValue after the loop
            formattedCSV.Append($"[{fieldValue.ToString().Trim()}]");

            // Return the formatted CSV as a string
            return formattedCSV.ToString().Trim();
        }
    }
}
