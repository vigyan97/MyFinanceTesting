using MyFinance.Models;
using MyFinance.Models.DtoModels;
using MyFinance.Services.IRepository;

namespace MyFinance.Services.Repository
{
    public class BankDetailsRepositoryService : IBankDetailsRepositoryService
    {
        private readonly MyFinanceContext _context;

        public BankDetailsRepositoryService(MyFinanceContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
        }
 
        public bool UpdateBankDetailsAsync(BankDetails bankDetails, BankDetailsDto bankDetailsDto)
        {
            var hasChange = false;
            if (bankDetailsDto.BankName != bankDetails.BankName ||
                bankDetailsDto.BranchName != bankDetails.BranchName ||
                bankDetailsDto.AccountNumber != bankDetails.AccountNumber ||
                bankDetailsDto.IFSC != bankDetails.IFSC)
            {
                hasChange = true;
            }
            return hasChange;
        }
    }
}
