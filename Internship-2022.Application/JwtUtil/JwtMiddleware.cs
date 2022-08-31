using Internship_2022.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2022.Application.JwtUtils
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var validationEntity = jwtUtils.ValidateToken(token);
            if (validationEntity!=null)
            {
                context.Items["User"] = await userService.GetUserById(validationEntity.Id);
            }

            await _next(context);
        }
    }
}
