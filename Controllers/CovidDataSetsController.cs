using System.Net;
using CovidDataSetsApi.Dto;
using CovidDataSetsApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CovidDataSetsApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CovidDataSetsController : ControllerBase
    {
        private readonly ILogger<CovidDataSetsController> _logger;
        private readonly IDataSetsRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public CovidDataSetsController(
            ILogger<CovidDataSetsController> logger,
            IDataSetsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }



        /// <summary>
        ///  Inserts a single record in to the CovidDataSets Table
        /// </summary>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        [HttpPost("InsertCovidDataSet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> InsertCovidDataSet([FromBody] CovidDataSetsDto dataSet)
        {
            try
            {
                return Ok(await _repository.InsertCovidDataSet(dataSet));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred while handling your request");
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Updates a single record in the CovidDataSets Table
        /// </summary>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        [HttpPut("UpdateCovidDataSet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateCovidDataSet([FromBody] CovidDataSetsDto dataSet)
        {
            try
            {
                return Ok(await _repository.UpdateCovidDataSetsRecord(dataSet));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred while handling your request");
                return StatusCode(500, "An error has occurred while handling your request");
            }

        }

        /// <summary>
        /// Deletes an entire data set from its respective table before deleting the record from
        /// the CovidDataSets table.
        /// </summary>
        /// <remarks>run the table's Purge endpoint in its respective repository</remarks>
        /// <param name="dataSetId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteCovidDataSet/{dataSetId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCovidDataSet(Guid dataSetId)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred while handling your request");
                return StatusCode(500, "An error has occurred while handling your request");
            }

        }






    }
}
