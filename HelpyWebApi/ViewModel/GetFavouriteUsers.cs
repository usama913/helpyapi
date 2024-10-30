using HelpyWebApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace HelpyWebApi.ViewModel
{
    public class GetFavouriteUsers
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? UGuid { get; set; }
        public bool IsActive { get; set; }
        public bool UserStatus { get; set; }
        public int SubscriptionId { get; set; }
        public string Gender { get; set; }
        public string? Occupation { get; set; }
        public int? Age { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Email { get; set; }
        public string Ethnicity { get; set; }
        public string Sexuality { get; set; }
        public string? Description { get; set; }

        public string? PhoneNumber { get; set; }
        public string SubscriptionName { get; set; }
        public decimal SubscriptionPrice { get; set; }
        public int SubsriptionDurationInDays { get; set; }
        public int? AgeRangeMin { get; set; }
        public int? AgeRangeMax { get; set; }
        public string? BodyType { get; set; }
        public string? Children { get; set; }
        public string? Drinking { get; set; }
        public string? Education { get; set; }
        public int? HeightInInches { get; set; }
        public string? Language { get; set; }
        public string? RelationshipStatus { get; set; }
        public string? Smoking { get; set; }
        public List<string> ImagePaths { get; set; }
        public List<string> Bills { get; set; }
        [AllowNull]
        public string City { get; set; }
        [AllowNull]
        public string Country { get; set; }
        [AllowNull]
        public string Longitude { get; set; }
        [AllowNull]
        public string Latitude { get; set; }
    }
}
