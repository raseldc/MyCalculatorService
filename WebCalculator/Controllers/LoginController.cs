using Microsoft.AspNetCore.Mvc;
using WebCalculator.Model;
using WebCalculator.Resources;
using WebCalculator.Security;
using WebCalculator.services;

namespace WebCalculator.Controllers
{
    [Route("Login")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly JwtAuthenticationManager _jwtAuthenticationManager;     

        public LoginController(JwtAuthenticationManager jwtAuthenticationManager, IUserService userService)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _userService = userService;
        }
        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            //return _userService.checkLogin(user.UserName,user.Password)==true?"Login Succesfully":"False";


            try
            {
                // Validate user credentials
                if (!_userService.checkLogin(user.UserName,user.Password))
                {
                    return Unauthorized("Invalid credentials");
                }


                // Generate JWT token
                var token = _jwtAuthenticationManager.GenerateToken(user.UserName);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing your request");
            }

        }
    }
}
