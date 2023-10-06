namespace ETicaretAPI.Client.Models.Communication
{
    public class CommunicationCustomerPerson
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public Guid CommunicationPersonId { get; set; }
        public bool IsSuccess { get; set; }
    }
}
