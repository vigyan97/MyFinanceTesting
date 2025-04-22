using System.Security.Claims;
using AutoMapper;
using MyFinance.DtoModels.Models;
using MyFinance.Models;
using MyFinance.Models.DtoModels;
using MyFinance.Services.IRepository;

namespace MyFinance.Services.Repository
{
    public class NomineeRepositoryService : INomineeRepositoryService
    {
        private readonly MyFinanceContext _context;

        public NomineeRepositoryService(MyFinanceContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
        }
 
        public bool UpdateNomineeAsync(Nominee nominee, NomineeDto nomineeDto)
        {
            bool hasChange = false;
            if (nominee.Relationship != nomineeDto.Relationship ||
                nominee.Dob != nomineeDto.Dob ||
                nominee.Name != nomineeDto.Name || 
                nominee.Address != nomineeDto.Address)
            {
                hasChange = true;
            }
            return hasChange;
        }
    }
}
