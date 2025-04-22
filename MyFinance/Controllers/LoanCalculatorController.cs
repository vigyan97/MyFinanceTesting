using Microsoft.AspNetCore.Mvc;
using MyFinance.Models.DtoModels;
using MyFinance.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFinance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class LoanCalculatorController : ControllerBase
    {
        // POST api/<LoanCalculatorController>
        [HttpPost("Monthly/")]
        public IActionResult PostMonthly([FromBody] LoanCalculatorInputs loanCalculatorInputs)
        {
            var principal = loanCalculatorInputs.Principal;
            var annualInterestRate = loanCalculatorInputs.AnnualInterestRate;
            var loanTermInMonths = loanCalculatorInputs.LoanTermMonths;

            List<LoanCalculatorOutput> outputs = [];
            double monthlyPayment = LoanCalculationService.CalculateMonthlyPayment(
                principal,
                annualInterestRate,
                loanTermInMonths);

            //monthlyPayment = Math.Round(monthlyPayment, 2);
            double extraValuesInOnesPlace = monthlyPayment % 10;
            double extraAmountInFirstInstallment = Math.Round(extraValuesInOnesPlace * (loanTermInMonths-1), 2);
            var firstPayment = Math.Round(monthlyPayment + extraAmountInFirstInstallment, 2);
            monthlyPayment= Math.Round(monthlyPayment - extraValuesInOnesPlace, 2);

            for (int i = 1; i <= loanTermInMonths; i++)
            {
                double monthlyPPMT = LoanCalculationService.CalculateMonthlyPPMT(
                principal,
                annualInterestRate,
                loanTermInMonths, i);
                monthlyPPMT = Math.Round(monthlyPPMT, 2);

                double monthlyIPMT = LoanCalculationService.CalculateMonthlyIPMT(
                principal,
                annualInterestRate,
                loanTermInMonths, i);                

                LoanCalculatorOutput inst;
                if (i == 1)
                {
                    monthlyIPMT = Math.Round(monthlyIPMT + extraAmountInFirstInstallment, 2);
                    inst = new LoanCalculatorOutput
                    {
                        InstallmentNo = i,
                        InstallmentAmount = firstPayment,
                        PrincipalInstallment = monthlyPPMT,
                        LoanInstallment = monthlyIPMT
                    };
                }
                else
                {
                    monthlyIPMT = Math.Round(monthlyIPMT - extraValuesInOnesPlace, 2);
                    inst = new LoanCalculatorOutput
                    {
                        InstallmentNo = i,
                        InstallmentAmount = monthlyPayment,
                        PrincipalInstallment = monthlyPPMT,
                        LoanInstallment = monthlyIPMT
                    };
                }
                outputs.Add(inst);
            }

            return Ok(outputs);
        }

        [HttpPost("Weekly/")]
        public IActionResult PostWeekly([FromBody] LoanCalculatorInputs loanCalculatorInputs)
        {
            List<LoanCalculatorOutput> outputs = [];
            var principal = loanCalculatorInputs.Principal;
            var annualInterestRate = loanCalculatorInputs.AnnualInterestRate;
            var loanTermInWeeks = loanCalculatorInputs.LoanTermWeeks;

            double weeklyPayment = LoanCalculationService.CalculateWeeklyPayment(
                principal,
                annualInterestRate,
                loanTermInWeeks);

            double extraValuesInOnesPlace = weeklyPayment % 10;
            double extraAmountInFirstInstallment = Math.Round(extraValuesInOnesPlace * (loanTermInWeeks - 1), 2);
            var firstPayment = Math.Round(weeklyPayment + extraAmountInFirstInstallment, 2);
            weeklyPayment = Math.Round(weeklyPayment - extraValuesInOnesPlace, 2);

            for (int i = 1; i <= loanTermInWeeks; i++)
            {
                double weeklyPPMT = LoanCalculationService.CalculateWeeklyPPMT(
                principal,
                annualInterestRate, loanTermInWeeks, i);
                weeklyPPMT = Math.Round(weeklyPPMT, 2);

                double weeklyIPMT= LoanCalculationService.CalculateWeeklyIPMT(
                principal,
                annualInterestRate, loanTermInWeeks, i);

                LoanCalculatorOutput inst;
                if (i == 1)
                {
                    weeklyIPMT = Math.Round(weeklyIPMT + extraAmountInFirstInstallment, 2);
                    inst = new LoanCalculatorOutput
                    {
                        InstallmentNo = i,
                        InstallmentAmount = firstPayment,
                        PrincipalInstallment = weeklyPPMT,
                        LoanInstallment = weeklyIPMT
                    };
                }
                else
                {
                    weeklyIPMT = Math.Round(weeklyIPMT - extraValuesInOnesPlace, 2);
                    inst = new LoanCalculatorOutput
                    {
                        InstallmentNo = i,
                        InstallmentAmount = weeklyPayment,
                        PrincipalInstallment = weeklyPPMT,
                        LoanInstallment = weeklyIPMT
                    };
                }
                outputs.Add(inst);
            }

            return Ok(outputs);
        }
    }
}
