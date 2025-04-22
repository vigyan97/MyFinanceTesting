using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFinance.Models
{
    [Table("TLoan")]
    public class Loan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanId { get; set; }
        public int MemberId { get; set; }

        [MaxLength(20)]
        public required string LoanType { get; set; }
        public double LoanAmount { get; set; }

        [MaxLength(50)]
        public required string PurposeOfLoan { get; set; }
        public int Tenure {  get; set; }
        public float RateOfInterest { get; set; }

        public int NoOfFamilyNumbers { get; set; }
        public double FamilyIncome { get; set; }
        public double FamilyExpenditure { get; set; }
        public double Obligation { get; set; }
        public double Surplus { get; set; }

        public required string CreatedBy {  get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsApproved { get; set; }

        [MaxLength(50)]
        public string? ApprovedBy { get; set; }
        [Column(TypeName = "datetime2(3)")]
        public DateTime ApprovedOn { get; set; }
        public bool IsDisbursed { get; set; }

        [MaxLength(50)]
        public string? DisbursedBy { get; set; }
        [Column(TypeName = "datetime2(3)")]
        public DateTime DisbursedOn { get; set; }

        [MaxLength(100)]
        public string? DisbursedVia { get; set; }
        public int ProcessingChargePercentage { get; set; }
        public double NetAmount { get; set; }

        public DayOfWeek? RecoveryDay { get; set; }

        [Range(1, 30)]
        public int? RecoveryDate { get; set; }
        public required bool IsRejected { get; set; }

        [MaxLength(50)]
        public string? RejectedBy { get; set; }
        public required bool IsClosed {  get; set; }

        [ForeignKey("MemberId")]
        public Member Member { get; set; } = null!;
    }
}
