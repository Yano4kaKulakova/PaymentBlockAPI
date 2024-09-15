namespace PaymentBlockAPI.Models
{
    public class Block
    {
        public Guid BlockId { get; set; }

        public string BlockDateTime { get; set; }

        public Guid ClientId { get; set; }

        public string Reason { get; set; }

        public string UnlockDateTime { get; set; } = " ";
    }
}
