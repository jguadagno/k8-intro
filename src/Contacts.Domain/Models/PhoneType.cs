using System.Diagnostics.CodeAnalysis;

namespace Contacts.Domain.Models;

[ExcludeFromCodeCoverage]

public class PhoneType
{
    public int PhoneTypeId { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
}