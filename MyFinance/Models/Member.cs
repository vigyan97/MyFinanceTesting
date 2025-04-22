using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFinance.Models;

[Table("TMember")]
public class Member : ModificationDetails
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MemberId { get; set; }

    [MinLength(4), MaxLength(50)]
    public required string FullName { get; set; }

    [MaxLength(50)]
    public string? FatherName { get; set; }
    [MaxLength(50)]
    public string? HusbandName { get; set; }

    [Required]
    public DateOnly Dob { get; set; }

    [MaxLength(10)]
    public string? Gender { get; set; }

    [MinLength(10),MaxLength(10)]
    public required string MobileNumber { get; set; }

    [MaxLength(50)]
    public string? CasteAndReligion { get; set; }

    [MinLength(12), MaxLength(12)]
    public required string AadharNumber { get; set; }

    [MaxLength(50)]
    public required string Occupation { get; set; }

    [MaxLength(15)]
    public required string MaritalStatus { get; set; }

    [MaxLength(15)]
    public string? PANNumber { get; set; }

    [MaxLength(50)]
    public string? EmailAddress { get; set; }
    public required int Income { get; set; }
    public required bool IsApproved { get; set; }

    [Column(TypeName = "datetime2(3)")]
    public DateTime OnboardedOn { get; set; }
    [Column(TypeName = "datetime2(3)")]
    public DateTime? ApprovedOn { get; set; }

    [MaxLength(50)]
    public string? IsApprovedBy { get; set; }

    [MaxLength(50)]
    public string? IsOnboardedBy { get; set; }
    public required bool IsDeleted { get; set; }

    [MaxLength(50)]
    public string? DeletedOrRejectedBy { get; set; }

    [MaxLength(50)]
    public required string ResidenceStatus { get; set; }
    public List<Address> Addresses { get; set; } = new List<Address>();
    public required Nominee Nominee { get; set; }
    public required BankDetails BankDetails { get; set; }
    public Loan? Loan { get; set; }
}
