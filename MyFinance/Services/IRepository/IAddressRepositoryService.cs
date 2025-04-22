using MyFinance.DtoModels.Models;
using MyFinance.Models;

namespace MyFinance.Services.IRepository
{
    public interface IAddressRepositoryService
    {
        Task<Address> AddAddressAsync(AddressDto AddressDto);
        bool UpdateAddressAsync(Address address, AddressDto addressDto);
    }
}
