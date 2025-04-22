using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyFinance.Models
{
    [Table("TBankDetails")]
    public class BankDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankDetailsId { get; set; }
        public int MemberId { get; set; }
        [MaxLength(100)]
        public required string BankName { get; set; }
        [MaxLength(100)]
        public required string BranchName { get; set; }
        [MaxLength(50)]
        public required string AccountNumber { get; set; }
        [MaxLength(50)]
        public required string IFSC { get; set; }

        [ForeignKey("MemberId")]
        public Member Member { get; set; } = null!;
    }
}
