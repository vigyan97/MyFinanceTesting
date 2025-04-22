using System.ComponentModel.DataAnnotations;

namespace MyFinance.Models.DtoModels
{
    public class RecoveryDto
    {
        public int RecoveryId { get; set; }
        public int LoanId { get; set; }
        public int InstallmentNo { get; set; }
        public double InstallmentAmount { get; set; }
        public double PrincipalInstallment { get; set; }
        public double LoanInstallment { get; set; }
        public DateTime RecoveryDate { get; set; }
        public double RecoveredAmount { get; set; }
        public DateTime RecoveryOn { get; set; }
        public DayOfWeek RecoveryDay { get; set; }

        [MaxLength(100)]
        public string? RecoveredVia { get; set; }

        [MaxLength(50)]
        public string? RecoveredBy { get; set; }
    }
}
