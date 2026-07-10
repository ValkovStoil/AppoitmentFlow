using AppoitmentFlow.API.CustomMassages;
using AppoitmentFlow.API.Data;
using AppoitmentFlow.API.DTOs.Auth;
using AppoitmentFlow.API.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;

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
    }
}
