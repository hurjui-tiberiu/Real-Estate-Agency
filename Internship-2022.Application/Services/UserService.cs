using AutoMapper;
using Internship_2022.Application.Interfaces;
using Internship_2022.Application.Models.UserDto;
using Internship_2022.Domain.Entities;
using Internship_2022.Domain.Enum;
using Internship_2022.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Internship_2022.Application.JwtUtils;
using Internship_2022.Application.Models.MailDto;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Internship_2022.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IJwtUtils jwtUtils;
        private readonly IMailService mailService;
        private readonly IAzureStorageService azureService;
        private readonly IListingRepository listingRepository;
        private readonly IFavoriteRepository favoriteRepository;

        public UserService(IUserRepository userRepo, IMapper mapper, IJwtUtils jwtUtils, 
            IMailService mailService, IAzureStorageService azureService, IListingRepository listingRepository,
            IFavoriteRepository favoriteRepository)
        {
            this.userRepository = userRepo;
            this.mapper = mapper;
            this.jwtUtils = jwtUtils;
            this.mailService = mailService;
            this.azureService = azureService;
            this.listingRepository = listingRepository;
            this.favoriteRepository = favoriteRepository;
        }

        public async Task<string> Authenticate(LoginModel login)
        {
            var user = await userRepository.GetUserByEmail(login.Email);

            if (user != null)
            {
                if (login.Password!= DecryptCipherTextToPlainText(user.Password))
                {
                    throw new Exception("Password is incorrect.");
                }
            }

            user.Token = jwtUtils.GenerateToken(user);
            await userRepository.UpdateUserById(user);
            return user.Token;
        }

        public async Task DeleteUserById(Guid id)
        {
            await userRepository.DeleteUserById(id);
        }

        public async Task<List<UserRequestDto>> GetAllUsers()
        {
            var users = await userRepository.GetAllUsers();

            return mapper.Map<List<UserRequestDto>>(users);
        }

        public async Task<UserRequestDto> GetUserById(Guid id)
        {
            var user = await userRepository.GetUserById(id);

            return mapper.Map<UserRequestDto>(user);
        }

        public async Task Register(CreateUserRequestDto userDto)
        {
            var user = mapper.Map<User>(userDto);

            user.FullName = "Anonim";
            user.Gender = EGender.Default;
            user.Phone = "0000-000-000";
            user.Role = ERole.User;
            user.NotificationPreferences = "None";
            user.DateOfBirth = DateTime.MinValue;
            user.Address = "-";
            user.CreatedAt = DateTime.Now;
            user.Password = EncryptPlainTextToCipherText(userDto.Password);

           await userRepository.AddUser(user);
        }

        public async Task ResetPassword(string email)
        {
            var user = await userRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new Exception("Wrong Mail");
            }

            user.Password = CreateNewPassword();
            MailRequest mailRequest = new MailRequest { ToEmail = user.Email, Subject = "New password", Body = user.Password };
            await mailService.SendEmailAsync(mailRequest);

            
            user.Password=EncryptPlainTextToCipherText(user.Password);
            await userRepository.UpdateUserById(user);
        }

        public async Task<User?> GetUserByEmail(string Email)
        {
            return await userRepository.GetUserByEmail(Email);
        }

        public async Task UpdateUserById(Guid Id, UpdateRequestDto userDto)
        {
            var user = await userRepository.GetUserById(Id);
            if (user != null)
            {
                var mappedUser = mapper.Map<UpdateRequestDto, User>(userDto, user);
                mappedUser.Photo = await azureService.UploadStreamUser(mappedUser.Photo);
                await userRepository.UpdateUserById(mappedUser);
                
            }
        }

        public async Task<List<UserRequestDto>> GetAdminsAsync()
        {
            var users = await userRepository.GetAllUsers();
            var admins = users.Where(entity => entity.Role == ERole.Admin);
            var adminsDto = mapper.Map<List<UserRequestDto>>(admins);

            return adminsDto;
        }

        private static string CreateNewPassword()
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();
            int size = random.Next(8, validChars.Length);
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            return new string(chars);
        }

        private const string SecurityKey = "ComplexKeyHere_12121";

        public static string EncryptPlainTextToCipherText(string PlainText)
        {
            byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(PlainText);

            var objMD5CryptoService = MD5.Create();
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = TripleDES.Create();
            objTripleDESCryptoService.Key = securityKeyArray;
            objTripleDESCryptoService.Mode = CipherMode.ECB;
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;


            var objCrytpoTransform = objTripleDESCryptoService.CreateEncryptor();
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
            objTripleDESCryptoService.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string DecryptCipherTextToPlainText(string CipherText)
        {
            byte[] toEncryptArray = Convert.FromBase64String(CipherText);
            var objMD5CryptoService = MD5.Create();

            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = TripleDES.Create();
            objTripleDESCryptoService.Key = securityKeyArray;
            objTripleDESCryptoService.Mode = CipherMode.ECB;
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCrytpoTransform = objTripleDESCryptoService.CreateDecryptor();
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            objTripleDESCryptoService.Clear();

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public async Task PatchUserByIdAsync(dynamic property, Guid userId)
        {
            var patchUserDto = JsonConvert.DeserializeObject<PatchUserRequestDto>(property.ToString());
            var user = await userRepository.GetUserById(userId);

            if (user != null)
            {
                var mappedUser = mapper.Map<PatchUserRequestDto, User>(patchUserDto, user);
                if (patchUserDto.Photo != null)
                {
                    mappedUser.Photo = await azureService.UploadStreamUser(mappedUser.Photo);
                }

                await userRepository.UpdateUserById(mappedUser);
            }
        }
    }
}


