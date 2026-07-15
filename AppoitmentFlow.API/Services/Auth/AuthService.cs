using AppoitmentFlow.API.CustomMassages;
using AppoitmentFlow.API.Data;
using AppoitmentFlow.API.DTOs.Auth;
using AppoitmentFlow.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppoitmentFlow.API.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(AppDbContext context)
        {
            _context = context;
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

            return new LoginResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                Token = string.Empty
            };
        }
    }
}
