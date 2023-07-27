using APIWizard.Sample.Common;
using Microsoft.AspNetCore.Mvc;

namespace APIWizard.Sample.ASPNETCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly ILogger<SampleController> _logger;
        private readonly IAPIClient _apiClient;

        public SampleController(ILogger<SampleController> logger, IAPIClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        [HttpGet]
        public async Task<Inventory> Get()
        {
            return await _apiClient.DoRequestAsync<Inventory>("/store/inventory", null, CancellationToken.None);
        }
    }
}