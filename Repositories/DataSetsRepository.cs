﻿using System.Text.Json.Nodes;
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



      

    }
}
