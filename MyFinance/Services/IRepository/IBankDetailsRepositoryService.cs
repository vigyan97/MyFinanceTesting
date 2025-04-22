using MyFinance.Models;
using MyFinance.Models.DtoModels;

namespace MyFinance.Services.IRepository
{
    public interface IBankDetailsRepositoryService
    {
        bool UpdateBankDetailsAsync(BankDetails bankDetails, BankDetailsDto bankDetailsDto);
    }
}
