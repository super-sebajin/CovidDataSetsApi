namespace CovidDataSetsApi.Dto
{
    public class CasesOvertimeUsDto
    {
        public Guid Id { get; set; }
        public Guid CovidDataSetId { get; set; }
        public DateTime DateStamp { get; set; }
        public int CountConfirmed { get; set; }
        public int CountDeath { get; set; }
        public int CountRecovered { get; set; }
    }
}
                                                  