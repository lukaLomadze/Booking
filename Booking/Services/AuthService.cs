using Azure;
using Booking.Data;
using Booking.Interfaces;
using Booking.Models.DTOs;
using Booking.Models.Entities;
using Booking.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Booking.Services
{
    public class AuthService : IAuthService
    {
        private Response Response { get; set; }

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<ResponseC> Register(UserRegisterDTO registerDTO)
        {

            if (registerDTO.UserRole != Enums.Role.Guest && registerDTO.UserRole != Enums.Role.Hoteladmin)
                return new ResponseC(false, "Unknown role");
            if (await UserExists(registerDTO.UserName)) return new ResponseC(false, "User already exists");
            if(registerDTO.ConfirmPassword != registerDTO.Password) return new ResponseC(false,"Passwords do not match" );
            CreatePasswordHash(registerDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User()
            {
                UserName = registerDTO.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = Enums.Status.Active,
                Role = registerDTO.UserRole,
                email = registerDTO.Email,
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new ResponseC(true, "User registered successfuly");


        }

        public async Task<ResponseT<string>> Login(UserLoginDTO loginDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginDTO.UserName.ToLower());
            if (user is null || user.Status != Enums.Status.Active)
            {
                return new ResponseT<string>(false, null, "User does not exists");
            }
            var res = new ResponseT<string>(true, null, $"Welcome {user.UserName}");
            if (!VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt)) 
                return new ResponseT<string>(false, null,"Incorrect password" );
            else
            {
                var result = GenerateTokens(user, loginDTO.StaySignedIn);
                res.Data = result.AccessToken;
            }

            if (loginDTO.StaySignedIn)
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            return res;
        }


       public async Task<ResponseT<List<User>>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return new ResponseT<List<User>>(true, users, "");
        }



        #region

        private async Task<bool> UserExists(string userName)
        {
            return await _context.Users.AnyAsync(x => x.UserName.ToLower() == userName.ToLower());
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private TokenDTO GenerateTokens(User user, bool staySignedIn)
        {
            string refreshToken = string.Empty;
            if (staySignedIn)
            {
                refreshToken = GenerateRefreshToken(user);
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpirationDate = DateTime.Now.AddDays(2);
            }
            var accessToken = GenerateAccessToken(user);
            return new TokenDTO { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        private string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Token:Secret").Value));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials,
                Issuer = "Booking",
                Audience = "BookingClient"
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private string GenerateRefreshToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {

                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Token:Secret").Value));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = credentials,
                Issuer = "Booking",
                Audience = "BookingClient"
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
        #endregion


    }
}
