using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DTOsTask.Service;

namespace DTOsTask.Controllers
{
    [ApiController] // Indicates that this class is an API controller, which automatically handles model validation and other HTTP-related tasks.
    [Route("api/[Controller]")] // Defines the route for the controller, where [Controller] will be replaced with the class name (in this case, "User").
    public class UserController : ControllerBase
    {
        // Declares the dependencies for the controller: IUserService for business logic and IConfiguration for accessing configuration settings.
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        // Constructor that initializes the dependencies through dependency injection.
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        // Action method for handling POST requests to add a new user.
        [HttpPost] // Specifies that this method will handle HTTP POST requests.
        public IActionResult addUser([FromBody] User user) // Accepts a User object from the request body.
        {
            try
            {
                // Calls the AddUser method from the injected IUserService to add the user.
                _userService.AddUser(user);
            }
            catch (Exception ex) // Catches any exception thrown during the user addition process.
            {
                // Returns a BadRequest response with the error message if an exception occurs.
                return BadRequest(ex.Message);
            }

            // Returns an Ok response with the user ID (UId) if the user was successfully added.
            return Ok(user.UId);
        }

        // Action method for handling GET requests to log in a user.
        [HttpGet] // Specifies that this method will handle HTTP GET requests.
        public IActionResult login(string email, string password) // Accepts email and password as query parameters.
        {
            // Calls the GetUser method from the injected IUserService to retrieve the user based on the provided email and password.
            var user = _userService.GetUser(email, password);

            if (user != null) // If the user is found, proceed to generate a JWT token.
            {
                // Generates a JWT token for the user and returns it in the response.
                string token = GenerateJwtToken(user.UId.ToString(), user.Name);
                return Ok(token); // Returns the token if the login is successful.
            }
            else
            {
                // Returns a BadRequest response if the provided credentials are invalid.
                return BadRequest("Invalid Credentials");
            }
        }

        // NonAction attribute indicates that this method is not an action method for handling HTTP requests.
        [NonAction]
        public string GenerateJwtToken(string userId, string username)
        {
            // Retrieves JWT settings from the configuration.
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"]; // Retrieves the secret key used for signing the JWT token.

            // Defines the claims (user data) that will be included in the JWT token.
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userId), // The subject of the token (user ID).
            new Claim(JwtRegisteredClaimNames.UniqueName, username), // The unique name (username).
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // A unique identifier for the JWT token (Jti).
        };

            // Creates a symmetric security key using the secret key.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Defines the signing credentials using HMAC SHA-256.

            // Creates the JWT token with the defined claims and expiry time from the configuration.
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])), // Expiry time in minutes retrieved from configuration.
                signingCredentials: creds
            );

            // Converts the JWT token to a string and returns it.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
