namespace PaymentBlockAPI.Models
{
    public class AddClientRequest
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public long Phone { get; set; }

        public string Address { get; set; }
    }
}
