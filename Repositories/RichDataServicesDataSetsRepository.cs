using System.Text.Json.Nodes;
using AutoMapper;

namespace CovidDataSetsApi.Repositories
{
    

    /// <summary>
    /// 
    /// </summary>
    public class RichDataServicesDataSetsRepository : IRichDataServicesDataSetsRepository
    {
        private readonly ILogger<RichDataServicesDataSetsRepository> _logger;
        private readonly HttpClient _httpClient;
        private readonly CovidDataSetsDbContext _db;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="db"></param>
        /// <param name="mapper"></param>
        public RichDataServicesDataSetsRepository(
            ILogger<RichDataServicesDataSetsRepository> logger,
            CovidDataSetsDbContext db,
            IMapper mapper)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _db = db;
            _mapper = mapper;
        }

        /// <summary>
        /// If CovidCasesOverTimeUsa table has not been populated yet, it will perform the operation to populate the table by calling the RDS API endpoint
        /// that is stored in the DB for that data set.
        /// </summary>
        /// <returns></returns>
        /// <author>Sebastian R. Papanikoloau-Costa</author>
        public async Task<GeneralResponse> PopulateCovidCasesOverTimeUsaTable(Guid dataSetId)
        {
            try
            {
                var getCasesOverTimeTable = await _db.CovidCasesOverTimeUsa.AsNoTracking().Select(x => x).ToListAsync();

                if (getCasesOverTimeTable.Count() == 0)
                {

                    var getDataToPopulateCasesOverTimeTable = _mapper.Map<List<CovidCasesOverTimeUsa>>(await GetCovidCasesOverTimeInTheUs(dataSetId));

                    await _db.CovidCasesOverTimeUsa.AddRangeAsync(getDataToPopulateCasesOverTimeTable);

                    await _db.SaveChangesAsync();

                    return new GeneralResponse { Success = true };

                }

                return new GeneralResponse
                {
                    Success = false,
                    Errors = "Table is already populated with the Data Set"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while populating the table for the first time");
                return new GeneralResponse
                {
                    Success = false,
                    Errors = ex.ToString()
                };
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// returns></returns>
        /// <author>Sebastian R. Papanikolaou-Costa<author>
        public async Task<List<CasesOvertimeUsDto>> GetVisualizeCOVID19CasesOverTimeInTheUsDataSet()
        {
            return _mapper.Map<List<CasesOvertimeUsDto>>(
                await _db.CovidCasesOverTimeUsa
                    .Select(x => x)
                    .ToListAsync());
        }




        /// <summary>
        /// Calls the RDS API endpoint that gets the "Visualize COVID-19 cases over time in the U.S." data set (json) and maps it from json to
        /// CasesOverTimeDto.
        /// </summary>
        /// <returns></returns>
        /// <author>Sebastian R. Papanikolaou-Costa</author>
        private async Task<List<CasesOvertimeUsDto>> GetCovidCasesOverTimeInTheUs(Guid dataSetId)
        {
            var dtoList = new List<CasesOvertimeUsDto>();
            var url = await _db.CovidDataSets.AsNoTracking().Where(ds => ds.Id == dataSetId).Select(d => d.DataSetPublicUrl).FirstOrDefaultAsync();

            var response = await _httpClient.GetStringAsync(url);
            var responseAsJson = JsonNode.Parse(response);

            foreach (var item in responseAsJson!["dataProvider"]!.AsArray())
            {
                dtoList.Add(
                    new CasesOvertimeUsDto
                    {
                        Id = Guid.NewGuid(),
                        CovidDataSetId = dataSetId,
                        DateStamp = item["date_stamp"]!.GetValue<DateTime>(),
                        CountConfirmed = item["cnt_confirmed"]!.GetValue<int>(),
                        CountDeath = item["cnt_death"]!.GetValue<int>(),
                        CountRecovered = item["cnt_recovered"]!.GetValue<int>()
                    }
                );
            }

            return dtoList;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GeneralResponse> PurgeTable()
        {
            try
            {   
                var getTable = _db.CovidCasesOverTimeUsa.Select(rec => rec);
                _db.CovidCasesOverTimeUsa.RemoveRange(getTable);
                await _db.SaveChangesAsync();

                return new GeneralResponse
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occurred: ");
                return new GeneralResponse
                {
                    Success = false,
                    Errors = ex.ToString()
                };
            }

        }
    }
}
