using MyFinance.Models;

namespace MyFinance.Services.IRepository
{
    public interface ILoanRepositoryService
    {
        Task<Loan> AddLoanAsync(Loan loan);
        Task<List<Loan>> GetAllApprovedLoansAsync();
        Task<Loan?> GetApprovedLoanbyIdAsync(int id);
        Task<Loan?> GetUnapprovedLoanbyIdAsync(int id);
        //Task<GetLoanDto?> GetApprovedUnapprovedLoanbyIdAsync(int id, bool isApproved);
        Task<List<Loan>> GetAllUnApprovedLoansAsync();
        Task<Loan?> GetLoanbyMemberIdAsync(int memberId);
        Task<bool> ApproveLoan(int loanId);
        Task UpdateLoanAsync(Loan loan);
        Task DeleteLoanAsync(Loan loan);
    }
}
