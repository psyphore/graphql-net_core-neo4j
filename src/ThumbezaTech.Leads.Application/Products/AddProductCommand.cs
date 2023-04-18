using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Application.Products;

public sealed record AddProductCommand(Product Product) : ICommand<Result>;

internal sealed class AddProductCommandHandler : ICommandHandler<AddProductCommand, Result>
{
  private readonly IProductService _service;

  public AddProductCommandHandler(IProductService service) => _service = service;

  public ValueTask<Result> Handle(AddProductCommand command, CancellationToken cancellationToken)
  {
    return _service.AddProduct(Guard.Against.Null(command.Product, nameof(command.Product)), cancellationToken);
  }
}
