using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFinance.Models
{
    [Table("TRecovery")]
    public class Recovery
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecoveryId { get; set; }
        public int LoanId { get; set; }
        public int InstallmentNo { get; set; }
        public double InstallmentAmount { get; set; }
        public double PrincipalInstallment { get; set; }
        public double LoanInstallment { get; set; }

        [Column(TypeName = "datetime2(3)")]
        public DateTime RecoveryDate { get; set; }

        public double RecoveredAmount { get; set; }

        [Column(TypeName = "datetime2(3)")]
        public DateTime RecoveryOn { get; set; }
        public DayOfWeek RecoveryDay { get; set; }

        [MaxLength(100)]
        public string? RecoveredVia { get; set; }

        [MaxLength(50)]
        public string? RecoveredBy { get; set; }

        [ForeignKey("LoanId")]
        public Loan Loan { get; set; } = null!;
    }
}
