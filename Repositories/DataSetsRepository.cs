using System.Text.Json.Nodes;
using AutoMapper;
using CovidDataSetsApi.DataAccessLayer;
using CovidDataSetsApi.Dto;
using CovidDataSetsApi.ResponseObjects;
using Microsoft.EntityFrameworkCore;

namespace CovidDataSetsApi.Repositories
{
    public interface IDataSetsRepository
    {

        Task<GenericResponse<CovidDataSetsDto>> InsertCovidDataSet(CovidDataSetsDto dataSetsDto);

        Task<List<CasesOvertimeUsDto>> GetCovidCasesOverTimeInTheUs(Guid dataSetId);
        Task<GeneralResponse> PopulateCovidCasesOverTimeUsaTable(Guid dataSetId);

        Task<GeneralResponse> UpdateCovidDataSetsRecord(CovidDataSetsDto dataSetsDto);
    }

    public class DataSetsRepository : IDataSetsRepository
    {

        private readonly ILogger<DataSetsRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly CovidDataSetsDbContext _db;
        private readonly IMapper _mapper;


        public DataSetsRepository(
            ILogger<DataSetsRepository> logger,
            IConfiguration configuration,
            CovidDataSetsDbContext db,
            IMapper mapper)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _db = db;
            _mapper = mapper;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <author>Sebastian R. Papanikoloau-Costa</author>
        public async Task<GenericResponse<CovidDataSetsDto>> InsertCovidDataSet(CovidDataSetsDto dataSetsDto)
        {

            try
            {

                _db.CovidDataSets.Add(_mapper.Map<CovidDataSets>(dataSetsDto));
                await _db.SaveChangesAsync();
                return new GenericResponse<CovidDataSetsDto>
                {
                    Success = true,
                    AffectedObject = dataSetsDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"");
                return new GenericResponse<CovidDataSetsDto>
                {
                    Success = false,
                    Errors = ex.ToString()
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSetsDto"></param>
        /// <returns></returns>
        /// <author>Sebastian R. Papanikoloau-Costa</author>
        public async Task<GeneralResponse> UpdateCovidDataSetsRecord(CovidDataSetsDto dataSetsDto)
        {
            try
            {
                var dataSetRecord =
                    await _db.CovidDataSets.Where(dataSet => dataSet.Id == dataSetsDto.Id).FirstOrDefaultAsync();
                
                dataSetRecord!.DataSetName = dataSetsDto.DataSetName;
                dataSetRecord.DataSetPublicUrl = dataSetsDto.DataSetPublicUrl!;
                dataSetRecord.DataSetPublicUrlHttpMethod = dataSetsDto.DataSetPublicUrlHttpMethod;

                await _db.SaveChangesAsync();


                return new GeneralResponse { Success = true};

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"");
                return new GeneralResponse
                {
                    Success = false,
                    Errors = ex.ToString()

                };
            }
        }

        //todo: finish CRUD methods for Data Set table records
        //public async Task<GeneralResponse> DeleteCovidDataSet()
        //{
        //    try
        //    {
        //        return new GeneralResponse();
        //    }
        //    catch (Exception ex)
        //    {
        //        return new GeneralResponse();
        //    }
        //}



        /// <summary>
        /// Calls the RDS API endpoint that gets the "Visualize COVID-19 cases over time in the U.S." data set (json) and maps it from json to
        /// CasesOverTimeDto.
        /// </summary>
        /// <returns></returns>
        /// <author>Sebastian R. Papanikolaou-Costa</author>
        public async Task<List<CasesOvertimeUsDto>> GetCovidCasesOverTimeInTheUs(Guid dataSetId)
        {
            var dtoList = new List<CasesOvertimeUsDto>();
            var url = await _db.CovidDataSets.AsNoTracking().Where(ds => ds.Id == dataSetId).Select(d => d.DataSetPublicUrl).FirstOrDefaultAsync();

            var response = await _httpClient.GetStringAsync(url);
            var responseAsJson = JsonNode.Parse(response);

            foreach (var item in responseAsJson["dataProvider"].AsArray())
            {
                dtoList.Add(
                    new CasesOvertimeUsDto
                    {   Id = Guid.NewGuid(),
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

                    return new GeneralResponse {Success = true};

                }

                return new GeneralResponse
                {
                    Success = false,
                    Errors = "Table is already populated with the Data Set"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"An error occurred while populating the table for the first time");
                return new GeneralResponse
                {
                  Success = false,
                  Errors = ex.ToString()
                };
            }
        }






    }
}
