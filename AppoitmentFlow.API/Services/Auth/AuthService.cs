using AppoitmentFlow.API.CustomMassages;
using AppoitmentFlow.API.Data;
using AppoitmentFlow.API.DTOs.Auth;
using AppoitmentFlow.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AppoitmentFlow.API.Services.Auth
{
    public class AuthService : IAuthService, IConfiguration
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public string? this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<User>();
        }
       

        public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO request)
        {
            var existingUser = _context.Users
                .Where(u => u.Email == request.Email)
                .FirstOrDefault();

            if (existingUser != null)
            {
                
                throw new Exception(CustomMasseges.EmailAlreadyExist);
            }

            if (request.Password != request.ConfirmPassword)
            {
                
                throw new Exception(CustomMasseges.PasswordDoNotMatch);
            }

            User user = new User();

            user.Email = request.Email;
            user.CreatedAt = DateTime.Now;


            user.PaswordHash = _passwordHasher.HashPassword(user, request.Password);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return new RegisterResponseDTO 
            { 
                Id = user.Id,
                Email = user.Email
                };
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                throw new Exception(CustomMasseges.InvalidEmailOrPasword);
            }

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PaswordHash, request.Password);

            if(passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                throw new Exception(CustomMasseges.InvalidEmailOrPasword);
            }

            var token = GenerateJwtToken(user);

            return new LoginResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                Token = token
            };
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        private string GenerateJwtToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Isssuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires:DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
