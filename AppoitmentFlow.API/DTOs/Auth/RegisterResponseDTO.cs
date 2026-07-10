namespace AppoitmentFlow.API.DTOs.Auth
{
    public class RegisterResponseDTO
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Message { get; set; } = "User registered Successfully";
    }
}
