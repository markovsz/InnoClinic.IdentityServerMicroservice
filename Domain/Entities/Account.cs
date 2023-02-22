using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Account : IdentityUser
    {
        public Guid? PhotoId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
