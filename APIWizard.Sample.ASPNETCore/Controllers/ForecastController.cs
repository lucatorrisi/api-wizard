using APIWizard.Sample.Common;
using Microsoft.AspNetCore.Mvc;

namespace APIWizard.Sample.ASPNETCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForecastController : ControllerBase
    {
        private readonly ILogger<ForecastController> _logger;
        private readonly IAPIClient _apiClient;

        public ForecastController(ILogger<ForecastController> logger, IAPIClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        [HttpGet]
        public async Task<Forecast> Get()
        {
            return await _apiClient.DoRequestAsync<Forecast>("forecast", null, CancellationToken.None);
        }
    }
}