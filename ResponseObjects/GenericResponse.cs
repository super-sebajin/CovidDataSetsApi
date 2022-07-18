namespace CovidDataSetsApi.ResponseObjects
{
    public class GenericResponse<TEntity> : GeneralResponse where TEntity : class 

    {
        public TEntity AffectedObject { get; set; }
    }
}
