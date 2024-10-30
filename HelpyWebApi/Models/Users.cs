using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HelpyWebApi.Models
{
    public class Users : IdentityUser<int>
    {
        public string? UGuid { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public bool UserStatus { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime PackageRenewalDate { get; set; }
        [MaxLength(50)]
        public string Gender { get; set; }

        [MaxLength(255)]
        public string? Occupation { get; set; }

        public int Age { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [MaxLength(100)]
        public string Ethnicity { get; set; }

        [MaxLength(50)]
        public string Sexuality { get; set; }

        public string? Description { get; set; }

        [MaxLength(50)]
        public string Type { get; set; }
        public string? City { get; set; }
        [MaxLength(255)]
        public string? Country { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
    }
}
