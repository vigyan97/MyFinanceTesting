namespace MyFinance.Services
{
    public static class LoanCalculationService
    {
        public static double CalculateMonthlyPayment(double principal, double annualInterestRate, int loanTermMonths)
        {
            // Convert annual interest rate to monthly interest rate
            double monthlyInterestRate = annualInterestRate / 12 / 100;

            // Calculate the monthly payment using the PMT formula
            double monthlyPayment = (principal * monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, loanTermMonths)) /
                                    (Math.Pow(1 + monthlyInterestRate, loanTermMonths) - 1);

            return monthlyPayment;
        }

        public static double CalculateWeeklyPayment(double principal, double annualInterestRate, int loanTermWeeks)
        {
            // Convert annual interest rate to weekly interest rate
            double weeklyInterestRate = annualInterestRate / 52 / 100;

            // Calculate the total number of weekly payments (loan term in weeks)
            int totalWeeks = loanTermWeeks;

            // Calculate the weekly payment using the PMT formula
            double weeklyPayment = (principal * weeklyInterestRate * Math.Pow(1 + weeklyInterestRate, totalWeeks)) /
                                   (Math.Pow(1 + weeklyInterestRate, totalWeeks) - 1);

            return weeklyPayment;
        }

        public static double CalculateMonthlyPPMT(double principal, double annualInterestRate, int loanTermMonths, int paymentNumber)
        {
            // Convert annual interest rate to monthly interest rate
            double monthlyInterestRate = annualInterestRate / 12 / 100;

            // Calculate the monthly payment (PMT) using the loan amortization formula
            double monthlyPayment = principal * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, loanTermMonths)) /
                                    (Math.Pow(1 + monthlyInterestRate, loanTermMonths) - 1);

            // Calculate the remaining balance before the payment is made
            double remainingBalance = principal * Math.Pow(1 + monthlyInterestRate, paymentNumber - 1) -
                                      monthlyPayment * (Math.Pow(1 + monthlyInterestRate, paymentNumber - 1) - 1) / monthlyInterestRate;

            // Calculate the interest for the current payment number
            double interestPayment = remainingBalance * monthlyInterestRate;

            // PPMT is the total monthly payment minus the interest portion
            double ppmt = monthlyPayment - interestPayment;

            return ppmt;
        }

        public static double CalculateMonthlyPPMT1(double principal, double annualInterestRate, int loanTermMonths, int paymentNumber)
        {
            // Convert annual interest rate to monthly interest rate
            double monthlyInterestRate = annualInterestRate / 12 / 100;

            // Calculate the principal payment (PPMT) for the specified payment number (paymentNumber)
            double ppmt = principal * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, loanTermMonths)) /
                          (Math.Pow(1 + monthlyInterestRate, loanTermMonths) - 1) -
                          principal * monthlyInterestRate * (Math.Pow(1 + monthlyInterestRate, paymentNumber - 1));

            return ppmt;
        }

        public static double CalculateMonthlyIPMT(double principal, double annualInterestRate, int loanTermMonths, int paymentNumber)
        {
            // Convert annual interest rate to monthly interest rate
            double monthlyInterestRate = annualInterestRate / 12 / 100;

            // Calculate the interest payment (IPMT) for the specified payment number (paymentNumber)
            double ipmt = principal * monthlyInterestRate *
                         (Math.Pow(1 + monthlyInterestRate, loanTermMonths) - Math.Pow(1 + monthlyInterestRate, paymentNumber - 1)) /
                         (Math.Pow(1 + monthlyInterestRate, loanTermMonths) - 1);

            return ipmt;
        }

        // Method to calculate the weekly PPMT (Principal Payment) for a specific payment number
        public static double CalculateWeeklyPPMT(double principal, double annualInterestRate, int loanTermWeeks, int paymentNumber)
        {
            // Convert annual interest rate to weekly interest rate
            double weeklyInterestRate = annualInterestRate / 52 / 100;

            // Calculate the weekly payment (PMT) using the amortization formula
            double weeklyPayment = (principal * weeklyInterestRate * Math.Pow(1 + weeklyInterestRate, loanTermWeeks)) /
                                   (Math.Pow(1 + weeklyInterestRate, loanTermWeeks) - 1);

            // Calculate the remaining balance before the payment
            double remainingBalance = principal * Math.Pow(1 + weeklyInterestRate, paymentNumber - 1) -
                                      weeklyPayment * (Math.Pow(1 + weeklyInterestRate, paymentNumber - 1) - 1) / weeklyInterestRate;

            // Calculate the interest for the current payment number
            double interestPayment = remainingBalance * weeklyInterestRate;

            // PPMT is the total weekly payment minus the interest portion
            double ppmt = weeklyPayment - interestPayment;

            return ppmt;
        }
        public static double CalculateWeeklyPPMT1(double principal, double annualInterestRate, int loanTermWeeks, int paymentNumber)
        {
            // Convert annual interest rate to weekly interest rate
            double weeklyInterestRate = annualInterestRate / 52 / 100;

            // Total number of weekly payments (loan term in weeks)
            int totalWeeks = loanTermWeeks;

            // Calculate the weekly principal payment (PPMT) for the specified payment number
            double ppmt = (principal * weeklyInterestRate * Math.Pow(1 + weeklyInterestRate, totalWeeks)) /
                          (Math.Pow(1 + weeklyInterestRate, totalWeeks) - 1) -
                          principal * weeklyInterestRate * Math.Pow(1 + weeklyInterestRate, paymentNumber - 1);

            return ppmt;
        }

        // Method to calculate the weekly IPMT (Interest Payment) for a specific payment number
        public static double CalculateWeeklyIPMT(double principal, double annualInterestRate, int loanTermWeeks, int paymentNumber)
        {
            // Convert annual interest rate to weekly interest rate
            double weeklyInterestRate = annualInterestRate / 52 / 100;

            // Total number of weekly payments (loan term in weeks)
            int totalWeeks = loanTermWeeks;

            // Calculate the weekly interest payment (IPMT) for the specified payment number
            double ipmt = principal * weeklyInterestRate *
                         (Math.Pow(1 + weeklyInterestRate, totalWeeks) - Math.Pow(1 + weeklyInterestRate, paymentNumber - 1)) /
                         (Math.Pow(1 + weeklyInterestRate, totalWeeks) - 1);

            return ipmt;
        }
    }
}
