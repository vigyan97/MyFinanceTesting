using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFinance.Models
{
    public class ModificationDetails
    {
        [Required]
        [Column(TypeName = "datetime2(3)")]
        public DateTime LastUpdatedOn { get; set; }

        [MinLength(4), MaxLength(50)]
        public required string LastUpdatedBy { get; set; }
    }
}
