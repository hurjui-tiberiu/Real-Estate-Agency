using Internship_2022.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2022.Application.Models.UserDto
{
    public class UpdateRequestDto
    {
        public string? Photo { get; set; }
        public string? FullName { set; get; }
        public EGender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Mail { get; set; }
        public string? Phone { get; set; }
    }
}
