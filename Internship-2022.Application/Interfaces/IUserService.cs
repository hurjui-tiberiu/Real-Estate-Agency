using Internship_2022.Application.Models.UserDto;
using Internship_2022.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2022.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> Authenticate(LoginModel login);
        Task Register(CreateUserRequestDto userDto);
        Task ResetPassword(string email);
        Task UpdateUserById(Guid Id, UpdateRequestDto user);
        Task<UserRequestDto> GetUserById(Guid id);
        Task<List<UserRequestDto>> GetAllUsers();
        Task DeleteUserById(Guid id);
        Task<User?> GetUserByEmail(string Email);
        Task<List<UserRequestDto>> GetAdminsAsync();
        Task PatchUserByIdAsync(dynamic property, Guid userId);
    }
}