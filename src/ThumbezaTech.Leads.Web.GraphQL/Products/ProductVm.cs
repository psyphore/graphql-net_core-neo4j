using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Web.GraphQL.Products;

public sealed record ProductVm
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Value { get; set; } = default!;
    public List<string> Tags { get; set; } = new();

    public static explicit operator ProductVm(Product v) => new()
    {
        Id = v.Id,
        Name = v.Name,
        Value = v.Value,
        Tags = v.Tags.ToList()
    };
}