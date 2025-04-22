using MyFinance.DtoModels.Models;
using MyFinance.Models;

namespace MyFinance.Services.IRepository
{
    public interface IMemberRepositoryService
    {
        Task<List<Member>> GetAllApprovedMembersAsync();
        Task<Member?> GetApprovedMemberbyIdAsync(int id);
        Task<List<Member>> GetAllUnApprovedMembersAsync();
        Task<Member?> GetUnApprovedMemberbyIdAsync(int id);
        Task<Member?> GetMemberbyIdAsync(int id);
        Task<Member> AddMemberAsync(Member member);
        Task DeleteMemberAsync(Member member);
        Task UpdateMemberAsync(Member member, MemberDto memberDto);
        Task<List<MemberSearchDto>> GetMembersByNameAsync(string name);
        Task<MemberSearchDto?> GetMemberbyMobileNumber(string mobileNumber);
        Task<MemberSearchDto?> GetMemberbyAadharNumber(string aadharNumber);
        Task<bool> ApproveOnboarding(int memberId);
    }
}
