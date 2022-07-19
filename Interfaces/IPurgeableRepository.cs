using CovidDataSetsApi.ResponseObjects;

namespace CovidDataSetsApi.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPurgeableRepository
    {   /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
        Task<GeneralResponse> PurgeTable();
    }
}
