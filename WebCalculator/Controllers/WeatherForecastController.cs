using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCalculator.Model;
using WebCalculator.services;

namespace WebCalculator.Controllers
{   

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       


        private readonly IEmailService _emailService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEmailService emailService)
        {
           _emailService = emailService;
         
        }

     

        [HttpGet(Name = "createTable")]
      //  [Authorize]
        public IActionResult Get()
        {

            //_emailService.SendEmailForPassword("shamiulgt@gmail.com", "123456");

            using (var context = new ApplicationDbContext())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();
                context.SaveChanges();
            }
            return Ok(new { Token = "Database Create Succesfully" });
        }
       


    }
    
}
