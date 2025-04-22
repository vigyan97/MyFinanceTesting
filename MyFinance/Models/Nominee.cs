using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFinance.Models
{
    [Table("TNominee")]
    public class Nominee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NomineeId { get; set; }
        public int MemberId { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(30)]
        public required string Relationship { get; set; }
        public DateOnly Dob { get; set; }

        [MaxLength(100)]
        public required string Address { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; } = null!;
    }
}
