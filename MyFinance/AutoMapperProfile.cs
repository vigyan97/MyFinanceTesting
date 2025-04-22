using AutoMapper;
using MyFinance.DtoModels.Models;
using MyFinance.Models;
using MyFinance.Models.DtoModels;

namespace MyFinance
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();

            CreateMap<Nominee, NomineeDto>();
            CreateMap<NomineeDto, Nominee>();

            CreateMap<BankDetails, BankDetailsDto>();
            CreateMap<BankDetailsDto, BankDetails>();
            
            CreateMap<Member, MemberDto>();
            CreateMap<MemberDto, Member>()
                .ForMember(dest => dest.Addresses, opt => opt.Ignore())
                .ForMember(dest => dest.BankDetails, opt => opt.Ignore())
                .ForMember(dest => dest.Nominee, opt => opt.Ignore());

            CreateMap<Loan, GetLoanDto>()
                .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.MemberId))
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.FullName))
                .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.Member.MobileNumber))
                .ForMember(dest => dest.PANNumber, opt => opt.MapFrom(src => src.Member.PANNumber));
            CreateMap<PostLoanDto, Loan>();

        }
    }
}
