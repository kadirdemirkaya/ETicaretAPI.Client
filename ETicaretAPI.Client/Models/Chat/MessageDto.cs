namespace ETicaretAPI.Client.Models.Chat
{
    public class MessageDto
    {
        public string message { get; set; }
        public bool PersonMessage { get; set; }
        public Guid CommunicationCustomerPersonId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? DeletedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool IsSuccess { get; set; } = true;
    }
}
