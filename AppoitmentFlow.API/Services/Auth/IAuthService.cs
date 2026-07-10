using AppoitmentFlow.API.DTOs.Auth;

namespace AppoitmentFlow.API.Services.Auth
{
    public interface IAuthService
    {

        Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO request);
    }
}