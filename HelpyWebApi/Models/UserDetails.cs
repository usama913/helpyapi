using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpyWebApi.Models
{
    public class UserDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? UGuid { get; set; }
        [Required]
        public int UserId { get; set; } // Foreign key from User
        public int? AgeRangeMin { get; set; }
        public int? AgeRangeMax { get; set; }

        [MaxLength(50)]
        public string BodyType { get; set; }

        [MaxLength(50)]
        public string? Children { get; set; }

        [MaxLength(50)]
        public string? Drinking { get; set; }

        [MaxLength(100)]
        public string? Education { get; set; }

        public int? HeightInInches { get; set; }

        [MaxLength(100)]
        public string Language { get; set; }

        [MaxLength(50)]
        public string? RelationshipStatus { get; set; }

        [MaxLength(50)]
        public string? Smoking { get; set; }
    }
}
