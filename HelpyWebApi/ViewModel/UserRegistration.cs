using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HelpyWebApi.ViewModel
{
    public class UserRegistration
    {
        public UserAdditionalData UseradditionalData { get; set; }
        public string UGuid { get; set; } = string.Empty;
        [Required]
        public int Age { get; set; }
        public string Birthday { get; set; }
        public string Description { get; set; }
        [Required]
        public string Email { get; set; }
        public string? Password { get; set; }
        [Required]
        public string Ethnicity { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Occupation { get; set; }
        [Required]
        public List<int> SelectedItems { get; set; }
        [Required]
        [Display(Name = "Upload Images")]
        [MinLength(1, ErrorMessage = "Please upload at least 1 image.")]
        [MaxLength(6, ErrorMessage = "You can upload a maximum of 6 images.")]
        public List<KeyValuePair<string, string>> Images { get; set; }
        [Required]
        public string Sexuality { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [AllowNull]
        public string Longitude { get; set; }
        [AllowNull]
        public string Latitude { get; set; }

    }
    public class UserAdditionalData
    {

        public List<int> AgeRange { get; set; }
        public string? BodyType { get; set; }
        public string? Children { get; set; }
        public string? Drinking { get; set; }
        public string? Education { get; set; }
        public int? HeightInInches { get; set; }
        public string Language { get; set; }
        public string? RelationshipStatus { get; set; }
        public string? Smoking { get; set; }
    }
}
