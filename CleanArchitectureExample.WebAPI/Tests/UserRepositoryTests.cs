using Moq;
using Xunit;
using CleanArchitectureExample.Application.Services;
using CleanArchitectureExample.Domain.Interfaces;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Infrastructure.Data;
using CleanArchitectureExample.Infrastructure.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using System.Threading;

namespace CleanArchitectureExample.WebAPI.Tests
{

    //Setup: Testiluokan konstruktorissa määritellään DbContextOptions<ApplicationDbContext>, joka osoittaa In-Memory -tietokantaan nimeltä "TestDb".
    //Jokainen testi luo oman ApplicationDbContext-instanssin näillä asetuksilla, varmistaen, että tietokanta on eristetty ja puhdas jokaista testiä varten.

    //Testimetodit: Jokainen testi luo uuden ApplicationDbContext-instanssin ja UserRepository-instanssin.
    //Testit suorittavat sitten yksittäisen operaation(kuten AddAsync tai EmailExistsAsync) ja tarkistavat lopputuloksen käyttäen Assert-luokan metodeita.
    //Esimerkiksi AddAsync_ShouldAddUser_WhenUserIsValid-testissä tarkistetaan, että käyttäjä on todella lisätty tietokantaan.

    public class UserRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public UserRepositoryTests()
        {
            //Konfiguroidaan In-Memory -tietokanta testien ajaksi:
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        [Fact]
        public async Task AddAsync_ShouldAddUser_WhenUserIsValid()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                //Arrange
                var userRepository = new UserRepository(context);
                var user = new User { Name = "Test User", Email = "test@example.com" };

                //Act
                await userRepository.AddAsync(user);

                //Assert
                var userInDb = await context.Users.FirstOrDefaultAsync(u => u.Email == "test@example.com");
                Assert.NotNull(userInDb);
                Assert.Equal("Test User", userInDb.Name);
            }
        }

        [Fact]
        public async Task EmailExistsAsync_ShouldReturnTrue_WhenEmailExists()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                //Arrange
                context.Users.Add(new User { Name = "Existing User", Email = "existing@example.com" });
                context.SaveChanges();

                var userRepository = new UserRepository(context);
                //Act 
                var exists = await userRepository.EmailExistsAsync("existing@example.com");

                //Assert
                Assert.True(exists);
            }
        }
    }
}
