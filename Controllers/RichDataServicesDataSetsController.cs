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


        /// <summary>
        /// Gets the contents of the  CovidCasesOverTimeUsa table in the db, if empty, will return an empty json
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetVisualizeCOVID19CasesOverTimeInTheUsDataSet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// Inserts in bulk, the data set for "Visualize COVID-19 cases over time in the U.S" in to its respective table.
        /// The Api assumes that a record for this data set exists in the CovidDataSets table.
        /// If CovidCasesOverTimeUsa table has not been populated yet,
        /// it will perform the operation to populate the table by calling the RDS API endpoint that is stored in the DB for that data set.
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
