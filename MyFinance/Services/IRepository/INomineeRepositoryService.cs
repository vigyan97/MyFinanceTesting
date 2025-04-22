using MyFinance.Models;
using MyFinance.Models.DtoModels;

namespace MyFinance.Services.IRepository
{
    public interface INomineeRepositoryService
    {
        bool UpdateNomineeAsync(Nominee nominee, NomineeDto nomineeDto);
    }
}
