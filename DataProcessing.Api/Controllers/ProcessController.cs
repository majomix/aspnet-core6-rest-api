using DataProcessing.Application.Contracts;
using DataProcessing.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataProcessing.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly IDataProcessorService _dataProcessorService;
        private readonly ILogger<DataJobController> _logger;

        public ProcessController(
            IDataProcessorService dataProcessorService,
            ILogger<DataJobController> logger)
        {
            _dataProcessorService = dataProcessorService;
            _logger = logger;
        }

        [HttpPost(Name = nameof(Start))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DataJobDto> Start([FromBody] Guid id)
        {
            _logger.LogInformation($"{nameof(Start)} invoked.");

            return Ok(_dataProcessorService.StartBackgroundProcess(id));
        }

        [HttpGet("{id:Guid}", Name = nameof(GetStatus))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DataJobStatusDto> GetStatus(Guid id)
        {
            _logger.LogInformation($"{nameof(GetStatus)} invoked.");

            return Ok(_dataProcessorService.GetBackgroundProcessStatus(id));
        }

        [HttpGet("{id:Guid}/Results", Name = nameof(GetResults))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<string>> GetResults(Guid id)
        {
            _logger.LogInformation($"{nameof(GetResults)} invoked.");

            return Ok(_dataProcessorService.GetBackgroundProcessResults(id));
        }
    }
}
