using CleanArchitectureExample.Application.Interfaces;
using CleanArchitectureExample.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureExample.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRegistrationService _registrationService;

        public UsersController(IUserRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public IActionResult RegisterUser(string name, string email)
        {
            //Syötteen validointi, eli tarkistetaan että nimi ja sähköposti on annettu

            //Tähän tarkistus, että sähköposti ei ole jo käytössä


            _registrationService.RegisterUserAsync(name, email);
            return new OkResult();
        }

        [HttpPost("UserAsync")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegistrationRequest request)
        {
               
            var isExistingEmail = await _registrationService.EmailExistsAsync(request.Email);

            if (isExistingEmail)
            {
                return BadRequest("Sähköpostiosoite on jo käytössä.");
            }

            var success = await _registrationService.RegisterUserAsync(request.Name, request.Email); // tähän muutettu RegisterUser tilalle RegisterUserAsync

            if (!success)
            {
                return BadRequest("Rekisteröinti epäonnistui.");
            }

            //Palautetaan onnistunut rekisteröinti:
            return Created();
        }
    }
}
