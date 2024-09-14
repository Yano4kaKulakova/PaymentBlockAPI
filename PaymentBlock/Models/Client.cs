namespace PaymentBlockAPI.Models
{
    public class Client
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public long Phone { get; set; }

        public string Address { get; set; }

        public string Status { get; set; } = "Активен";
    }
}
