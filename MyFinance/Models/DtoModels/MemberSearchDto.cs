namespace MyFinance.DtoModels.Models;

public class MemberSearchDto
{
    public int MemberId { get; set; }
    public string FullName { get; set; } = null!;

    //public DateOnly Dob { get; set; }
    public string? Gender { get; set; }
    public required string MobileNumber { get; set; }
    public bool? HasApprovedLoan {  get; set; }
    public int? LoanId { get; set; } 
}