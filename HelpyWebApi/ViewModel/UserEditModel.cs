using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace HelpyWebApi.ViewModel
{
    public class UserEditModel
    {
        public int userId { get; set; }
        public string Description { get; set; }
        public List<KeyValuePair<string, string>?>? Images { get; set; }
        public string FullName { get; set; }
        public string Sexuality { get; set; }
        public string Occupation { get; set; }
        public string Gender { get; set; }
        public string Ethnicity { get; set; }
        public string Language { get; set; }
        public string BodyType { get; set; }
        public string RelationshipStatus { get; set; }
        public string Smoking { get; set; }
        public string Children { get; set; }
        public string Drinking { get; set; }
        public string Education { get; set; }
        public int? HeightInInches { get; set; }
        public DateTime? Birthday { get; set; }
        public List<int>? SelectedItems { get; set; }
    }
}
