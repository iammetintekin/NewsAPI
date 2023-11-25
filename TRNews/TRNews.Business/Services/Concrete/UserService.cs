using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TRNews.Business.Services.Abstract;
using TRNews.Entity.DTOs.Identity;
using TRNews.Entity.Models;
using TRNews.Entity.ReturnObjects;

namespace TRNews.Business.Services.Concrete
{
    public class UserService : IUserService
    { 
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager; 
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContext;
        public UserService(UserManager<User> userManager, SignInManager<User> signInManager,IMapper mapper, IConfiguration config, IHttpContextAccessor httpContext)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _httpContext = httpContext;
        }
        private User? _user; 
        public async Task<Result> RegisterUser(CreateUserDto Model)
        {
            var user = _mapper.Map<User>(Model);
            user.NormalizedEmail = Model.Email.ToLower().ToUpperInvariant();
            user.UserName = Model.Email;
            user.NormalizedUserName = Model.Email.ToLower().ToUpperInvariant();

            var result = _userManager.CreateAsync(user, Model.Password).Result;
            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, Model.Roles);
            else
                return new Result("Hata : bilgilerinizi kontrol ediniz.", false); 

            return new Result("Kayıt başarılı", true);
        }

        public async Task<Result> ValidateUser(LoginUserDto Model)
        {
            _user = await _userManager.FindByNameAsync(Model.Username);
            var result = (_user != null && _signInManager.CheckPasswordSignInAsync(_user, Model.Password, true).Result.Succeeded);
            if (!result)
                return new Result($"Hatalı kullanıcı adı veya şifre {Model.Username} ", false);
            return new Result($"Giriş başarılı {Model.Username} ", true);
        }

        public async Task<TokenDto> CreateToken(bool populateExp=false)
        {
            var signInCredential = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signInCredential, claims);

            var refreshToken = GenerateRefreshToken();
            _user.RefreshToken = refreshToken;

            if (populateExp)
                _user.RefreshTokenExpireTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(_user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new TokenDto { AccessToken = accessToken, RefreshToken = refreshToken };
        }
        public async Task<TokenDto> RefreshToken(TokenDto Model)
        {
            var principles = GetPrincipalFromExpiredToken(Model.AccessToken);
            var user = await _userManager.FindByNameAsync(principles.Identity.Name);
            if (user is null || user.RefreshToken != Model.RefreshToken || user.RefreshTokenExpireTime <= DateTime.Now)
                throw new Exception("Reason : User is null, Refresh token doesn't equal or refresh token time is expired");
            _user = user;
            return await CreateToken(populateExp: false);
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signInCredential, List<Claim> claims)
        {
            var jwtSeciton = _config.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSeciton["validIssuer"],
                audience: jwtSeciton["validAudience"],
                expires: DateTime.Now.AddMinutes(double.Parse(jwtSeciton["expires"])),
                signingCredentials: signInCredential,
                claims: claims);

            return tokenOptions;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName) // kullanıcı bilgileri ve role bilgileri claim olarak eklenir.
            };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            return claims;
        }
        private SigningCredentials GetSigningCredentials()
        {
            var jwtSeciton = _config.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSeciton["secretKey"]);
            var secret = new SymmetricSecurityKey(secretKey);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        // süresi dolmuş olan tokendan kullanıcı bilgilerini alacağımız fonksiyon
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSeciton = _config.GetSection("JwtSettings");

            var secretKey = jwtSeciton["secretKey"];
            var validIssuer = jwtSeciton["validIssuer"];
            var validAudience = jwtSeciton["validAudience"];

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = validIssuer,
                ValidAudience = validAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principle = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            var tokenConverted = validatedToken as JwtSecurityToken;

            if (tokenConverted is null ||  !tokenConverted.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new Exception("Invalid validate token");
            }
            return principle;
        }

        public async Task<User> CurrentUser()
        {
            var loggeduser = _httpContext.HttpContext.User;
            var userId = loggeduser.FindFirstValue(ClaimTypes.Name);
            var result = await _userManager.FindByNameAsync(userId);
            return result;
        }
    }
}
