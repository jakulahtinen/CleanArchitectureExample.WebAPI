using Moq;
using Xunit;
using CleanArchitectureExample.Application.Services;
using CleanArchitectureExample.Domain.Interfaces;
using CleanArchitectureExample.Domain.Entities;
using System.Threading.Tasks;

namespace CleanArchitectureExample.WebAPI.Tests
{
    public class UserRegistrationServiceTests
    {

        //Arrange: Alustetaan tarvittavat mock-oliot ja UserRegistrationService instanssi. Moq-kirjastoa käytetään IUserRepository-rajapinnan simuloimiseen.
        //Määritämme mockin käyttäytymisen EmailExistsAsync-metodille palauttamaan true ensimmäisessä testissä ja false toisessa testissä.
        //Toisessa testissä määrittelemme myös, että AddAsync-metodi simuloituu onnistuneesti suoritetuksi.

        //Act: Suoritetaan RegisterUserAsync-metodi testidatalla.

        //Assert: Tarkistetaan, että metodi palauttaa odotetun arvon.Ensimmäisessä testissä, koska simuloimme tilanteen, jossa sähköpostiosoite on jo käytössä,
        //odotamme metodin palauttavan false. Toisessa testissä odotamme true-arvoa, mikä indikoi onnistunutta rekisteröintiä.

        //Muista testeissä aina!: Arrange, Act, Assert!
        [Fact]
        public async Task RegisterUserAsync_ReturnFalse_IfEmailExists()
        {
            //Arrange
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.EmailExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var service = new UserRegistrationService(mockRepo.Object);

            //Act, syötetään testidata:
            var result = await service.RegisterUserAsync("Test User", "test@example.com");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task RegisterUserAsync_ReturnsTrue_IfRegistrationSucceeds()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.EmailExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            var service = new UserRegistrationService(mockRepo.Object);

            //Act, syötetään testidata:
            var result = await service.RegisterUserAsync("New User", "new@example.com");

            //Assert
            Assert.True(result);
        }
    }
}
