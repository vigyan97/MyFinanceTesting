namespace MyFinance.Models.DtoModels
{
    public class LoanCalculatorInputs
    {
        public required double Principal {  get; set; }

        public required double AnnualInterestRate { get; set; }

        //public int loanTermYears = Convert.ToInt32(Console.ReadLine());

        public int LoanTermMonths {  get; set; }
        public int LoanTermWeeks { get; set; }
    }
}
