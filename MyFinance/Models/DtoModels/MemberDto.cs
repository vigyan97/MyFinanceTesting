using System.ComponentModel;
using MyFinance.Models.DtoModels;

namespace MyFinance.DtoModels.Models;

public class MemberDto
{
    [ReadOnly(true)]
    public int MemberId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string? Gender { get; set; }

    public required string MobileNumber { get; set; }

    public required string AadharNumber { get; set; }
    public string? FatherName { get; set; }
    public string? HusbandName { get; set; }
    public string? CasteAndReligion { get; set; }
    public required string Occupation { get; set; }
    public required string MaritalStatus { get; set; }
    public string? PANNumber { get; set; }
    public string? EmailAddress { get; set; }
    public required int Income { get; set; }
    public bool IsApproved { get; set; }
    public string? IsApprovedBy { get; set; }
    public string? IsOnboardedBy { get; set; }

    [ReadOnly(true)]
    public DateTime OnboardedOn { get; set; }
    public DateTime? ApprovedOn { get; set; }
    public required string ResidenceStatus { get; set; }
    public required List<AddressDto> Addresses { get; set; }

    public required NomineeDto Nominee { get; set; }
    public required BankDetailsDto bankDetails { get; set; }
    //public bool? HasApprovedLoan {  get; set; }
    //public int? LoanId { get; set; } 
}