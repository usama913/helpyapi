using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HelpyWebApi.Models
{
    public class Subscriptions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(19,4)")]
        public decimal DefaultPrice { get; set; }

        public bool IsActive { get; set; }

        public int? DurationInDays { get; set; }
    }
}
