using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFinance.Models;

[Table("TAddress")]
public class Address : ModificationDetails
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AddressId { get; set; }

    public int MemberId { get; set; }

    [MinLength(4), MaxLength(50)]
    public required string AddressLine { get; set; }

    [MinLength(4), MaxLength(50)]
    public required string City { get; set; }

    [MinLength(4), MaxLength(50)]
    public required string District { get; set; }

    [MinLength(4), MaxLength(50)]
    public required string State { get; set; }

    [MinLength(6),MaxLength(6)]
    public required int Pincode { get; set; }

    public bool IsPermanent { get; set; } = false;

    public bool IsCurrent { get; set; } = false;

    [ForeignKey("MemberId")]
    public Member Member { get; set; } = null!;
}
