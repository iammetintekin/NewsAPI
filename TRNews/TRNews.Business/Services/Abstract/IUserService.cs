using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRNews.Entity.DTOs.Identity;
using TRNews.Entity.Models;
using TRNews.Entity.ReturnObjects;

namespace TRNews.Business.Services.Abstract
{
    public interface IUserService
    {
        Task<Result> RegisterUser(CreateUserDto Model);
        Task<Result> ValidateUser(LoginUserDto Model);
        Task<TokenDto> CreateToken(bool populateExp = false);
        Task<TokenDto> RefreshToken(TokenDto Model);
        Task<User> CurrentUser();

    }
}
