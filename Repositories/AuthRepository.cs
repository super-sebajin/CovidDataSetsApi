
namespace CovidDataSetsApi.Repositories
{
    public class AuthRepository: IAuthRepository 
    {
        private readonly CovidDataSetsDbContext _db;
        private readonly IMapper _mapper;
        public AuthRepository(
            CovidDataSetsDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <summary>
        /// Searches DB for a User by Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<UserDto?> FindByUsernameAsync(UserCredentialsDto credentials) 
        {
            var user = await _db.User.Where(x => x.Username == credentials.Username).Select(x => x).FirstOrDefaultAsync();
            if (credentials.Username == null)
            {
                throw new ArgumentNullException(nameof(credentials.Username));
            }
            else if (user == null) 
            {
                return null;
            }
            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public async Task<UserActionResponse> RegisterUserAsync(UserCredentialsDto credentials)
        {
            if (credentials.Username == null || credentials.Password == null) 
            {
                return new UserActionResponse
                {
                    Success = false,
                    Errors = $"Both {nameof(credentials.Username)} and {nameof(credentials.Password)} must have values"
                };
            }

            var usernameGet = await FindByUsernameAsync(credentials);

            if (usernameGet == null)
            {
                var newUser = new User
                {
                    Username = credentials.Username,
                    Password = HashPassword(credentials.Password),
                    IsActive = false
                };
                await _db.User.AddAsync(newUser);
                await _db.SaveChangesAsync();
                return new UserActionResponse
                {
                    Success = true,
                    Username = credentials.Username,
                };
            }
            else 
            {
                return new UserActionResponse 
                {
                    Success = false,
                    Errors = $"There seems to be a user already registered under the username '{credentials.Username}'"
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<UserActionResponse> LoginUserAsync(UserCredentialsDto credentials) 
        {
            if (credentials.Username == null || credentials.Password == null)
            {
                if (credentials.Username == null && credentials.Password != null)
                {
                    throw new ArgumentException(nameof(credentials.Username));
                }
                else if (credentials.Password == null && credentials.Username != null)
                {
                    throw new ArgumentException(nameof(credentials.Password));
                }
                else if (credentials.Username == null && credentials.Password == null) 
                {
                    throw new ArgumentException(
                        nameof(credentials.Username), nameof(credentials.Password));
                }
            }

            var usernameGet = await FindByUsernameAsync(credentials);

            if (usernameGet == null)
            {
                return new UserActionResponse
                {
                    Success = false,
                    Errors = $"There exists no user with the username '{credentials.Username}'"
                };
            }
            else if (await VerifyPassword(credentials) != true) 
            {
                return new UserActionResponse
                {
                    Success = false,
                    Errors = $"Wrong password!"
                };
            }
            var userToActivate = await _db.User.Where(usr => usr.Username == credentials.Username).Select(usr => usr).FirstOrDefaultAsync();
            userToActivate!.IsActive = true;
            await _db.SaveChangesAsync();
            return new UserActionResponse
            {
                Success = true,
                Username = credentials.Username
            };


        }

        /// <summary>
        /// Used upon registering a user and when resetting a password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string HashPassword(string password) 
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Verifies whether the password is correct
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public async Task<bool> VerifyPassword(UserCredentialsDto credentials)
        {
            var userInDb = await _db.User.Where(usr => usr.Username == credentials.Username).Select(u => u).FirstOrDefaultAsync();
            if (userInDb != null)
                return BCrypt.Net.BCrypt.Verify(credentials.Password, userInDb.Password);
            else return false;
        }
    }
}
