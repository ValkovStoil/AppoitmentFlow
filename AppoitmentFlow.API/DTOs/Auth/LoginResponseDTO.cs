namespace AppoitmentFlow.API.DTOs.Auth
{
    public class LoginResponseDTO
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
    }
}
