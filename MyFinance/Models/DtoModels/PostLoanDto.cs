using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyFinance.Models.DtoModels
{
    public class PostLoanDto
    {
        public int LoanId { get; set; }
        public int MemberId { get; set; }
        public required string LoanType { get; set; }
        public required string PurposeOfLoan { get; set; }
        public int NoOfFamilyNumbers { get; set; }
        public double FamilyIncome { get; set; }
        public double FamilyExpenditure { get; set; }
        public double Obligation { get; set; }
        public double Surplus { get; set; }
        public double LoanAmount { get; set; }
        public int ProcessingChargePercentage { get; set; }
        public int Tenure { get; set; }
        public float RateOfInterest { get; set; }
        public bool IsRejected { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDisbursed { get; set; }
        public string? DisbursedVia { get; set; }
        public double NetAmount { get; set; }
        public DayOfWeek? RecoveryDay { get; set; }

        [Range(1,30)]
        public int? RecoveryDate { get; set; }
        public required bool IsClosed { get; set; }
    }
}
