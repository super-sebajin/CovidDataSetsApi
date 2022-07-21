namespace CovidDataSetsApi.Dto
{
    public class CovidDataSetsDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string DataSetName { get; set; }
        public string? DataSetPublicUrl { get; set; }
        public string DataSetPublicUrlHttpMethod { get; set; }
        public string DataSetProviderLongName { get; set; }
        public string DataSetProviderShortName { get; set; }


    }
}
                                                                           