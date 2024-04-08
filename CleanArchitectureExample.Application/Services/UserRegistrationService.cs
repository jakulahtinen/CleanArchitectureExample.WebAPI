using CleanArchitectureExample.Application.Interfaces;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRepository _userRepository;

        public UserRegistrationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //public void RegisterUser(string name, string email)
        //{
        //    var user = new User { Id = Guid.NewGuid(), Name = name, Email = email };
        //    _userRepository.Add(user);
        //}

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepository.EmailExistsAsync(email);
        }

        public async Task<bool> RegisterUserAsync(string name, string email)
        {
            try
            {
                if (await EmailExistsAsync(email))
                {
                    return false; //Sähköpostiosoite on jo käytössä!
                }

                var user = new User { Id = Guid.NewGuid(), Name = name, Email = email };
                await _userRepository.AddAsync(user);
                return true; //Rekisteröinti onnistui!
            }
            catch (ApplicationException)
            {
                return false;
            }
        }
        //YLIMÄÄRÄISET
        //bool IUserRegistrationService.RegisterUser(string name, string email)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool EmailExists(string email)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
