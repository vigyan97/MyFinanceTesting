using System.ComponentModel.DataAnnotations;

namespace MyFinance.Models.DtoModels
{
    public class NomineeDto
    {
        public int NomineeId { get; set; }
        public int MemberId { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(30)]
        public required string Relationship { get; set; }
        public DateOnly Dob { get; set; }
        public required string Address { get; set; }
    }
}
