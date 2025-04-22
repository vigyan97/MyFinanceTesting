using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyFinance.DtoModels.Models;

public class AddressDto
{
    [ReadOnly(true)]
    public int AddressId { get; set; }
    [ReadOnly(true)]
    public int MemberId { get; set; }
    public required string AddressLine { get; set; }
    public required string City { get; set; }
    public required string District { get; set; }
    public required string State { get; set; }
    public int Pincode { get; set; }
    public bool IsPermanent { get; set; }
    public bool IsCurrent { get; set; }

}
