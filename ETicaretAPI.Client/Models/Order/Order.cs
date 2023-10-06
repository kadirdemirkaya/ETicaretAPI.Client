namespace ETicaretAPI.Client.Models.Order
{
    public class Order
    {
        public string? Description { get; set; }

        public Guid? CustomerId { get; set; }
        public Guid? AddressId { get; set; }

        public Guid? Id { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? DeletedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool? IsSuccess { get; set; }
    }
}
