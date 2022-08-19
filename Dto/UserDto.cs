namespace CovidDataSetsApi.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }   
        public bool IsActive { get; set; }
    }
}
