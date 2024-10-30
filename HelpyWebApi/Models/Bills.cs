using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HelpyWebApi.Models
{
    public class Bills
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
