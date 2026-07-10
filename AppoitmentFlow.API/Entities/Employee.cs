namespace AppoitmentFlow.API.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid BusinessId { get; set; }
    }
}
