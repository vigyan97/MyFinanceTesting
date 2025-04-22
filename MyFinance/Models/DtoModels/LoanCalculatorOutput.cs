namespace MyFinance.Models.DtoModels
{
    public class LoanCalculatorOutput
    {
        public int InstallmentNo { get; set; }
        public double InstallmentAmount {  get; set; }
        public double PrincipalInstallment { get; set; }
        public double LoanInstallment { get; set; }
    }
}
