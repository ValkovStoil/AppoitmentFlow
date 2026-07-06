namespace AppoitmentFlow.API.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }

        public string CustomerName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
