using CleanArchitectureExample.Application.Dtos;
using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Mappers
{
    public class UserMapper
    {
        public UserDto? ToDto(User user)
        {
            if (user == null)
            {
                return null; // Return null if user is null
            }

            return new UserDto
            {
                Email = user.Email,
                Name = user.Name
            };
        }
    }
}
