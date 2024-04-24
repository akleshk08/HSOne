using CSVWebServiceAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSVWebServiceAPI.Controllers
{    
    [ApiController]
    [Route("[controller]")]
    public class CSVController : ControllerBase
    {
        private readonly ICSVService _csvService;
        private readonly ILogger _logger;

        public CSVController(ICSVService csvService, ILogger<CSVController> logger)
        {
            _csvService = csvService;
            _logger = logger;
        }


        [HttpPost(Name = "FormatCSV")]
        public IActionResult FormatCSV([FromBody] string inputCSV)
        {
            try
            {
                // Call the FormatCSV method of the CSVService
                string formattedCSV = _csvService.FormatCSV(inputCSV);

                // Return the formatted CSV string
                return Ok(formattedCSV);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while formatting CSV data.");

                // Return a 500 Internal Server Error with the error message
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
