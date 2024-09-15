namespace PaymentBlockAPI.Models
{
    public class AddBlockRequest
    {
        public string Reason { get; set; }

        Client Client { get; set; }

    }
}
