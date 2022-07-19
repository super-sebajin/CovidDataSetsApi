using CovidDataSetsApi.Dto;
using CovidDataSetsApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CovidDataSetsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CovidDataController : ControllerBase
    {
        private readonly ILogger<CovidDataController> _logger;
        private readonly IDataSetsRepository _repository;

        public CovidDataController(
            ILogger<CovidDataController> logger,
            IDataSetsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }



        /// <summary>
        /// 
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
        /// 
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






    }
}
