namespace ThumbezaTech.Leads.Domain.ProductAggregate.Events;

public sealed class ProductCreatedEvent : BaseDomainEvent
{
    public Product Product { get; set; }
    public ProductCreatedEvent(Product product) => Product = product;
}
