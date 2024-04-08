using CleanArchitectureExample.Application.Interfaces;
using CleanArchitectureExample.WebAPI.Controllers;
using CleanArchitectureExample.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;



namespace CleanArchitectureExample.WebAPI.Tests
{
    public class UserControllerTests
    {
        //Arrange-vaiheessa luodaan Moq-kirjastolla mock IUserRegistrationService-palvelusta ja määritellään, miten se käyttäytyy,
        //kun RegisterUserAsync-metodia kutsutaan: palautetaan true onnistuneelle rekisteröinnille ja false epäonnistuneelle. Luodaan myös UsersController-instanssi, joka saa riippuvuutenaan mock-palvelun.

        //Act-vaiheessa suoritetaan testattava metodi RegisterUserAsync syöttämällä sille testidata.

        //Assert-vaiheessa varmistetaan, että metodi palauttaa odotetun tuloksen: CreatedResult onnistuneen rekisteröinnin jälkeen ja
        //BadRequestObjectResult epäonnistuneen rekisteröinnin jälkeen.



        [Fact]
        public async Task RegisterUserAsync_ReturnsCreatedResult_WhenRegistrationSucceeds()
        {
            // Arrange

            //Mockataan IUserRegistrationService-rajapinnan RegisterUserAsync-metodi
            var mockService = new Mock<IUserRegistrationService>();

            //Simuloi rekisteröintiä, joka onnistuu:
            mockService.Setup(service => service.RegisterUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true); //Simuloi onnistunutta rekisteröintiä, eli jos email ja nimi on "string" niin rekisteröinti onnistuu.

            //Luodaan UsersController-olio ja annetaan sille mockattu IUserRegistrationService-olio
            var controller = new UsersController(mockService.Object);

            //Act, Syötetään testidata
            var result = await controller.RegisterUserAsync(new UserRegistrationRequest { Name = "Testi", Email = "testi@test.com" });

            //Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task RegisterUserAsync_ReturnsBadRequest_WhenRegistrationFails()
        {
            //Arrange
            var mockService = new Mock<IUserRegistrationService>();

            //Simuloidaan rekisteröintiä, joka epäonnistuu:
            mockService.Setup(service => service.RegisterUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false); // Simuloi epäonnistunutta rekisteröintiä

            var controller = new UsersController(mockService.Object);

            //Act, Syötetään testidata
            var result = await controller.RegisterUserAsync(new UserRegistrationRequest { Name = "Testi", Email = "fail@test.com" });

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
