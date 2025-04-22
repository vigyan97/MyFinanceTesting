using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyFinance.DtoModels.Models;
using MyFinance.Models;
using MyFinance.Services.IRepository;

namespace MyFinance.Services.Repository
{
    public class MemberRepositoryService : IMemberRepositoryService
    {
        private readonly ILogger<MemberRepositoryService> _logger;
        private readonly MyFinanceContext _context;
        private readonly IAddressRepositoryService _addressRepositoryService;
        private readonly INomineeRepositoryService _nomineeRepositoryService;
        private readonly IBankDetailsRepositoryService _bankDetailsRepositoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string? user;
        private readonly IMapper _mapper;

        public MemberRepositoryService(ILogger<MemberRepositoryService> logger, 
            MyFinanceContext context,
            IAddressRepositoryService addressRepositoryService, 
            INomineeRepositoryService nomineeRepositoryService, 
            IBankDetailsRepositoryService bankDetailsRepositoryService,
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _addressRepositoryService = addressRepositoryService;
            _nomineeRepositoryService = nomineeRepositoryService;
            _bankDetailsRepositoryService = bankDetailsRepositoryService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            user = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            user = user?.Split('@')[0];
        }

        public async Task<List<Member>> GetAllApprovedMembersAsync()
        {
            var members = await _context.Members.Where(x => x.IsDeleted == false && x.IsApproved == true).ToListAsync();
            return members;
        }

        public async Task<Member?> GetApprovedMemberbyIdAsync(int id)
        {
            var member = await _context.Members.Include(x => x.Addresses).Include(x => x.BankDetails).Include(x => x.Nominee)
                .FirstOrDefaultAsync(x => x.MemberId == id 
                && x.IsDeleted == false 
                && x.IsApproved == true);
            return member;
        }

        public async Task<List<Member>> GetAllUnApprovedMembersAsync()
        {
            var members = await _context.Members.Where(x => x.IsDeleted == false 
            && x.IsApproved == false 
            && x.IsOnboardedBy != user).ToListAsync();
            return members;
        }

        public async Task<Member?> GetUnApprovedMemberbyIdAsync(int id)
        {
            var member = await _context.Members.Include(x => x.Addresses).Include(x => x.BankDetails).Include(x => x.Nominee)
                .FirstOrDefaultAsync(x => x.MemberId == id 
                && x.IsDeleted == false 
                && x.IsApproved == false 
                && x.IsOnboardedBy != user);
            return member;
        }

        public async Task<Member?> GetMemberbyIdAsync(int id)
        {
            var member = await _context.Members.Include(x => x.Addresses).Include(x => x.BankDetails).Include(x => x.Nominee)
                .FirstOrDefaultAsync(x => x.MemberId == id && x.IsDeleted == false);
            return member;
        }

        public async Task<Member> AddMemberAsync(Member member)
        {
            var dateTimeNow = DateTime.Now;

            foreach (var address in member.Addresses)
            {
                address.LastUpdatedBy = user;
                address.LastUpdatedOn = dateTimeNow;
            }
            member.IsApproved = false;
            member.LastUpdatedBy = user;
            member.LastUpdatedOn = dateTimeNow;
            member.IsOnboardedBy = user;
            member.OnboardedOn = dateTimeNow;
            member.IsDeleted = false;

            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }


        public async Task DeleteMemberAsync(Member member)
        {
            member.IsDeleted = true;
            member.DeletedOrRejectedBy = user;
            //_context.Members.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMemberAsync(Member member, MemberDto memberDto)
        {
            member.LastUpdatedBy = user;
            member.LastUpdatedOn = DateTime.Now;

            foreach(var address in memberDto.Addresses)
            {
                var existingAddress = member.Addresses.FirstOrDefault(x => x.AddressId == address.AddressId);
                if (existingAddress != null)
                {
                    var hasAddChange = _addressRepositoryService.UpdateAddressAsync(existingAddress, address);
                    if (hasAddChange)
                        existingAddress = _mapper.Map(address,existingAddress);
                }
            }
            var hasChange = _nomineeRepositoryService.UpdateNomineeAsync(member.Nominee, memberDto.Nominee);
            if (hasChange)
                _mapper.Map(memberDto.Nominee, member.Nominee);

            hasChange = _bankDetailsRepositoryService.UpdateBankDetailsAsync(member.BankDetails, memberDto.bankDetails);
            if (hasChange)
                _mapper.Map(memberDto.bankDetails, member.BankDetails);

            await _context.SaveChangesAsync();
        }

        public async Task<List<MemberSearchDto>> GetMembersByNameAsync(string name)
        {
            var members = await _context.Members.Where(x => x.IsApproved == true && x.FullName.Contains(name)).Select(m =>
            new MemberSearchDto
            {
                MemberId = m.MemberId,
                FullName = m.FullName,
                Gender = m.Gender,
                MobileNumber = m.MobileNumber,
                LoanId = m.Loan != null && m.Loan.IsClosed == false ? m.Loan.LoanId : null,
                HasApprovedLoan = m.Loan != null ? m.Loan.IsApproved : null

            }).ToListAsync();
            return members;
        }

        public async Task<MemberSearchDto?> GetMemberbyMobileNumber(string mobileNumber)
        {
            var member = await _context.Members.Where(x => x.IsApproved == true && x.MobileNumber.Equals(mobileNumber)).Select(m =>
            new MemberSearchDto
            {
                MemberId = m.MemberId,
                FullName = m.FullName,
                Gender = m.Gender,
                MobileNumber = m.MobileNumber,
                LoanId = m.Loan != null ? m.Loan.LoanId : null,
                HasApprovedLoan = m.Loan != null ? m.Loan.IsApproved : null

            }).FirstOrDefaultAsync();
            return member;
        }

        public async Task<MemberSearchDto?> GetMemberbyAadharNumber(string aadharNumber)
        {
            var member = await _context.Members.Where(x => x.IsApproved == true && x.AadharNumber.Equals(aadharNumber)).Select(m =>
            new MemberSearchDto
            {
                MemberId = m.MemberId,
                FullName = m.FullName,
                Gender = m.Gender,
                MobileNumber = m.MobileNumber,
                LoanId = m.Loan != null ? m.Loan.LoanId : null,
                HasApprovedLoan = m.Loan != null ? m.Loan.IsApproved : null

            }).FirstOrDefaultAsync();
            return member;
        }

        public async Task<bool> ApproveOnboarding(int memberId)
        {
            var member = await GetUnApprovedMemberbyIdAsync(memberId);
            if (member == null)
                return false;
            member.IsApproved = true;
            member.IsApprovedBy = user;
            member.ApprovedOn = DateTime.Now;
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
