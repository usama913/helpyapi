using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HelpyWebApi.Models
{
    public class FilterUserModel
    {
        public int? userId { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        [AllowNull]
        public string BodyType { get; set; }
        [AllowNull]
        public string Children { get; set; }
        [AllowNull]
        public string Drinking { get; set; }
        [AllowNull]
        public string Education { get; set; }
        [AllowNull]
        public string Ethnicity { get; set; }
        [AllowNull]
        public string FullName { get; set; }
        [AllowNull]
        public string Gender { get; set; }
        public int? HeightInInches { get; set; }
        [AllowNull]
        public string Language { get; set; }
        [AllowNull]
        public string Occupation { get; set; }
        [AllowNull]
        public string RelationshipStatus { get; set; }
        public List<int>? SelectedItems { get; set; }
        [AllowNull]
        public string Sexuality { get; set; }
        [AllowNull]
        public string Smoking { get; set; }
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
