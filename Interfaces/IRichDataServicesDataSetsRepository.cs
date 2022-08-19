using CovidDataSetsApi.Dto;
using CovidDataSetsApi.ResponseObjects;

namespace CovidDataSetsApi.Interfaces
{
    public interface IRichDataServicesDataSetsRepository : IPurgeableRepository
    {
        Task<GeneralResponse> PopulateCovidCasesOverTimeUsaTable(Guid dataSetId);
        Task<List<CasesOvertimeUsDto>> GetVisualizeCOVID19CasesOverTimeInTheUsDataSet();
    }
}
