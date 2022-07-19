using Microsoft.AspNetCore.Mvc;
using CovidDataSetsApi.Repositories;
namespace CovidDataSetsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RichDataServicesDataSetsController : ControllerBase
    {
        private readonly IRichDataServicesDataSetsRepository _repository;
        private readonly ILogger<RichDataServicesDataSetsController> _logger;
        public RichDataServicesDataSetsController(
            IRichDataServicesDataSetsRepository repository,
            ILogger<RichDataServicesDataSetsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        [HttpGet("GetVisualizeCOVID19CasesOverTimeInTheUsDataSet")]
        public async Task<ActionResult> GetVisualizeCOVID19CasesOverTimeInTheUsDataSet()
        {
            try
            {
                return Ok(await _repository.GetVisualizeCOVID19CasesOverTimeInTheUsDataSet());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred with your request");
                return StatusCode(500, "An error has occurred with your request");
            }
        }



        /// <summary>
        /// In the meantime, should insert (in bulk) the entire RDS data set returned by the HTTP GET method that is
        /// stored in the DB for the {datasetId}.
        /// </summary>
        /// <param name="dataSetId"></param>
        /// <returns></returns>
        [HttpPost("InsertCasesOverTimeInUsaDataSet/{dataSetId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> InsertCasesOverTimeInUsaDataSet(Guid dataSetId)
        {
            try
            {
                return Ok(await _repository.PopulateCovidCasesOverTimeUsaTable(dataSetId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred with your request");
                return StatusCode(500, "An error has occurred with your request");
            }
        }



    }
}
