using CovidDataSetsApi.Dto;
using CovidDataSetsApi.ResponseObjects;

namespace CovidDataSetsApi.Interfaces
{
    public interface ICovidDataSetsRepository
    {
        Task<CovidDataSetsDto> InsertCovidDataSet(CovidDataSetsDto dataSetsDto);

        Task<GeneralResponse> UpdateCovidDataSetsRecord(CovidDataSetsDto dataSetsDto);

        Task<List<CovidDataSetsDto>> GetAllCovidDataSets();
    }
}
