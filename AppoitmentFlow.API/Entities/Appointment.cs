namespace AppoitmentFlow.API.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
