using AutoMapper;
using DataProcessing.Api.Models;
using DataProcessing.Application.Contracts;
using DataProcessing.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataProcessing.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataJobController : ControllerBase
    {
        private readonly IDataProcessorService _dataProcessorService;
        private readonly IMapper _mapper;
        private readonly ILogger<DataJobController> _logger;

        public DataJobController(
            IDataProcessorService dataProcessorService,
            IMapper mapper,
            ILogger<DataJobController> logger)
        {
            _dataProcessorService = dataProcessorService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = nameof(GetAllDataJobs))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<DataJobDto>> GetAllDataJobs()
        {
            _logger.LogInformation($"{nameof(GetAllDataJobs)} invoked.");

            return Ok(_dataProcessorService.GetAllDataJobs());
        }

        [HttpGet("{status}", Name = nameof(GetDataJobsByStatus))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<DataJobDto>> GetDataJobsByStatus(DataJobStatusDto status)
        {
            _logger.LogInformation($"{nameof(GetDataJobsByStatus)} invoked.");
            
            return Ok(_dataProcessorService.GetDataJobsByStatus(status));
        }

        [HttpGet("{id:Guid}", Name = nameof(GetDataJobById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DataJobDto> GetDataJobById(Guid id)
        {
            _logger.LogInformation($"{nameof(GetDataJobById)} invoked.");

            return Ok(_dataProcessorService.GetDataJob(id));
        }

        [HttpPost(Name = nameof(CreateDataJob))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DataJobDto> CreateDataJob([FromBody] DataJobCreateInput dataJob)
        {
            _logger.LogInformation($"{nameof(CreateDataJob)} invoked.");

            var dto = _mapper.Map<DataJobDto>(dataJob);

            return Ok(_dataProcessorService.Create(dto));
        }

        [HttpPut(Name = nameof(UpdateDataJob))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DataJobUpdateInput> UpdateDataJob([FromBody] DataJobUpdateInput dataJob)
        {
            _logger.LogInformation($"{nameof(UpdateDataJob)} invoked.");

            var dto = _mapper.Map<DataJobDto>(dataJob);

            return Ok(_dataProcessorService.Update(dto));
        }

        [HttpDelete("{id:Guid}", Name = nameof(DeleteDataJob))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteDataJob(Guid id)
        {
            _logger.LogInformation($"{nameof(DeleteDataJob)} invoked.");

            _dataProcessorService.Delete(id);

            return NoContent();
        }
    }
}