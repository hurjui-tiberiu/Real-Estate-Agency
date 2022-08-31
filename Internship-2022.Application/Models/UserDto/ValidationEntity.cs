using Internship_2022.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2022.Application.Models.UserDto
{
    public class ValidationEntity
    {
        public string Email { get; set; }
        public ERole Role { get; set; }
        public Guid Id { get; set; }
    }
}
