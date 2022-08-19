using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace CovidDataSetsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        //private readonly JwtAuthenticationManager _jwtAuthenticationManager;
        private ILogger<AuthController> _logger;

        public AuthController(
            IAuthRepository authRepository,
            //JwtAuthenticationManager jwtAuthenticationManager,
            ILogger<AuthController> logger)
        {
            _authRepository = authRepository;
            //_jwtAuthenticationManager = jwtAuthenticationManager;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login([FromBody] string username)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpPost("Authorize")]
        public async Task<IActionResult> AuthorizeUser([FromBody] User user)
        {
            string? token = null;//_jwtAuthenticationManager.Authenticate(user.Username, user.Password);
            if (token == null) 
            {
                return Unauthorized();
            }
            return Ok(token);
        }



        
    }
}
