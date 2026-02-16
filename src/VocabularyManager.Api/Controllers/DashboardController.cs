using Microsoft.AspNetCore.Mvc;
using VocabularyManager.UseCases.Interfaces;

namespace VocabularyManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private const int DefaultTopCount = 5;
        private readonly IDashboardMetricsProvider _dashboardMetricsProvider;

        public DashboardController(IDashboardMetricsProvider dashboardMetricsProvider)
        {
            _dashboardMetricsProvider = dashboardMetricsProvider;
        }

        [HttpGet("top-words")]
        public async Task<IActionResult> GetTopWordsByDefinitionCount([FromQuery] int count = DefaultTopCount, CancellationToken cancellationToken = default)
        {
            var items = await _dashboardMetricsProvider.GetTopWordsByDefinitionCountAsync(count, cancellationToken);
            return Ok(items);
        }

        [HttpGet("top-vocabularies")]
        public async Task<IActionResult> GetTopVocabulariesByWordCount([FromQuery] int count = DefaultTopCount, CancellationToken cancellationToken = default)
        {
            var items = await _dashboardMetricsProvider.GetTopVocabulariesByWordCountAsync(count, cancellationToken);
            return Ok(items);
        }
    }
}
