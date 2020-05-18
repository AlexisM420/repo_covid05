namespace BillingManagement.Models
{
    public class ContactInfo
    {
        public string ContactType { get; set; }
        public string Contact { get; set; }

        public int ContactInfoId { get; set; }

        public string Info => $"{ContactType} : {Contact}";
    }
}