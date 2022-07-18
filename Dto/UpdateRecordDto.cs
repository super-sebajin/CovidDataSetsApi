namespace CovidDataSetsApi.Dto
{
    public class UpdateRecordDto<T>
    {
        public Guid Id { get; set; }
        public string ColumnName { get; set; }

        public T NewValue { get; set; }

    }
}
