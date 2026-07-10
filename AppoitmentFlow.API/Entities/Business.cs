namespace AppoitmentFlow.API.Entities
{
    public class Business
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid OwnerId { get; set; }

        public string Owner { get; set; } = string.Empty;
    }
}
