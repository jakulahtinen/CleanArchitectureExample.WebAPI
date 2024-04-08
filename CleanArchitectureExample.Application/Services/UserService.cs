using CleanArchitectureExample.Application.Dtos;
using CleanArchitectureExample.Application.Mappers;
using CleanArchitectureExample.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserMapper _userMapper;

        public UserService(IUserRepository userRepository, UserMapper userMapper)
        {
            _userRepository = userRepository;
            _userMapper = userMapper;
        }

        public UserDto GetUserByEmail(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            return _userMapper.ToDto(user);
        }
    }
}
