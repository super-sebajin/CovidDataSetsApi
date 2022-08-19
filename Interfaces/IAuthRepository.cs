using CovidDataSetsApi.DataAccessLayer;
namespace CovidDataSetsApi.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserDto?> FindByUsernameAsync(UserCredentialsDto credentials);
        Task<UserActionResponse> RegisterUserAsync(UserCredentialsDto credentials);
        Task<UserActionResponse> LoginUserAsync(UserCredentialsDto credentials);
    }
}
