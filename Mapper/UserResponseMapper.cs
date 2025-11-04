using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dtos.Response;
using EcommerceApi.Models;

namespace EcommerceApi.Mapper
{
    public class UserResponseMapper
    {
        public static UserResponseDto ToUserResponseDto(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Roles = user.Roles.Where(r => r.Name.HasValue).Select(r => r.Name!.Value).ToList(),
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}