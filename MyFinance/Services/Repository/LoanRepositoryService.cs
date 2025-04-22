using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MyFinance.Models;
using MyFinance.Services.IRepository;

namespace MyFinance.Services.Repository
{
    public class LoanRepositoryService : ILoanRepositoryService
    {
        private readonly MyFinanceContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string? user;
        public LoanRepositoryService(MyFinanceContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            user = user?.Split('@')[0];
        }
        public async Task<Loan> AddLoanAsync(Loan loan)
        {
            loan.IsApproved = false;
            loan.IsDisbursed = false;
            loan.IsClosed = false;
            loan.CreatedBy = user;
            loan.CreatedOn = DateTime.Now;
            loan.NetAmount = loan.LoanAmount * ((100 - loan.ProcessingChargePercentage) / 100);
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<List<Loan>> GetAllApprovedLoansAsync()
        {
            var loans = await _context.Loans.Where(x => x.IsApproved == true).Include(y => y.Member).ToListAsync();
            return loans;
        }

        public async Task<bool> ApproveLoan(int loanId)
        {
            var loan = await GetUnapprovedLoanbyIdAsync(loanId);
            if (loan == null)
                return false;
            loan.IsApproved = true;
            loan.ApprovedBy = user;
            loan.ApprovedOn = DateTime.Now;
            //_context.Loans.Update(loan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Loan>> GetAllUnApprovedLoansAsync()
        {
            var loans = await _context.Loans.Where(x => x.IsApproved == false 
            && x.IsRejected==false && x.CreatedBy!=user).Include(y => y.Member).ToListAsync();
            return loans;
        }

        public async Task<Loan?> GetApprovedLoanbyIdAsync(int id)
        {
            var loan = await _context.Loans.Include(y => y.Member).SingleOrDefaultAsync(x => x.IsApproved == true
             && x.CreatedBy != user && x.LoanId==id);
            
            return loan;
        }

        public async Task<Loan?> GetLoanbyMemberIdAsync(int memberId)
        {
            var loan = await _context.Loans.SingleOrDefaultAsync(x => x.MemberId == memberId);
            return loan;
        }

        public async Task<Loan?> GetUnapprovedLoanbyIdAsync(int id)
        {
            var loan = await _context.Loans.Include(y => y.Member).SingleOrDefaultAsync(x => x.IsApproved == false
             && x.IsRejected==false && x.CreatedBy != user && x.LoanId == id);
            return loan;
        }

        public async Task UpdateLoanAsync(Loan loan)
        {
            //_context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLoanAsync(Loan loan)
        {
            loan.IsRejected = true;
            loan.RejectedBy = user;
            //_context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }
    }
}
