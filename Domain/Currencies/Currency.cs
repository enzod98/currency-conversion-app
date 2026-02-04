
using Domain.Abstractions;

namespace Domain.Currencies;

public class Currency : Entity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public decimal RateToBase { get; set; }
}
