using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Account : IdentityUser
    {
        public Guid PhotoId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
