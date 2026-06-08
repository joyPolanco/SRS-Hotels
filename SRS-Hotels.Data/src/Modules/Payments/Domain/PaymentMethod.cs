namespace SRS_Hotels.Data.src.Modules.Payments.Domain
{
    namespace SRS_Hotels.Modules.Payments.Domain
    {
        public class PaymentMethod
        {
            public Guid Id { get; set; }

            public required string Name { get; set; }
        }
    }
}
