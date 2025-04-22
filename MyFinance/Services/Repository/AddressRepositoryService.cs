using System.Security.Claims;
using AutoMapper;
using MyFinance.DtoModels.Models;
using MyFinance.Models;
using MyFinance.Services.IRepository;

namespace MyFinance.Services.Repository
{
    public class AddressRepositoryService : IAddressRepositoryService
    {
        private readonly MyFinanceContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string? user;
        private readonly IMapper _mapper;

        public AddressRepositoryService(MyFinanceContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor; 
            _mapper = mapper;
            user = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            user = user?.Split('@')[0];
        }
        public async Task<Address> AddAddressAsync(AddressDto addressDto)
        {
            var address = _mapper.Map<Address>(addressDto);
            address.LastUpdatedBy = user;
            address.LastUpdatedOn = DateTime.Now;

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public bool UpdateAddressAsync(Address address, AddressDto addressDto)
        {
            bool hasChange = false;
            if (address.AddressLine != addressDto.AddressLine ||
                address.City != addressDto.City ||
                address.District != addressDto.District ||
                address.State != addressDto.State ||
                address.Pincode != addressDto.Pincode)
            {
                hasChange = true;
            }

            return hasChange;
        }
    }
}
