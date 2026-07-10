namespace AppoitmentFlow.API.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PaswordHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

    }
}
