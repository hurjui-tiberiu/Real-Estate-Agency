using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2022.Application.Models.UserDto
{
    public class CreateUserRequestDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
